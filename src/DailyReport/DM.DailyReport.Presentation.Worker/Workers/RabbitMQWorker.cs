using System.Text;
using System.Text.Json;
using DM.Application.Reports.Worker;
using DM.SharedKernel.Enums;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DM.DailyReport.Presentation.Worker.Workers;

public sealed class RabbitMqWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IConnection _connection;
    private readonly IMediator _mediator;
    private readonly IModel _channel;

    public RabbitMqWorker(
        IServiceScopeFactory serviceScopeFactory,
        IConnection connection,
        IMediator mediator
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
        _connection = connection;
        _mediator = mediator;
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: nameof(Queues.EntryCreatedQueue),
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }
    
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        //Essa não é a forma mais elegante usar delegate, mas serve como POC
        consumer.Received += async (model, ea) =>
        {
            using var scope = _serviceScopeFactory.CreateScope();

            try 
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var entry = JsonSerializer.Deserialize<ProcessEntryCommand>(message);

                if (entry is null) return;
                await _mediator.Send(entry, cancellationToken);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            catch (Exception e)
            {
                /*
                 * TODO: aqui seria uma ótima opção para salvar Logs
                 * utilizando correlationId
                 */
            }
        };

        _channel.BasicConsume(
            queue: nameof(Queues.EntryCreatedQueue),
            autoAck: false, //Deixe AutoAck pra retirar da fila, mas deixe FALSO caso queira deburar sem perder a mensagem
            consumer: consumer
        );
        
        return Task.CompletedTask;
    }

    //Não esquecer de fechar a conexão após consumo.
    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        
        base.Dispose();
    }
}