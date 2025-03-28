using System.Text;
using System.Text.Json;
using DM.SharedKernel.Enums;
using MediatR;
using RabbitMQ.Client;

namespace DM.Application.Entries.Notifications;

public sealed class EntryCreatedNotificationHandler : INotificationHandler<EntryCreatedNotification>
{
    private readonly IModel _channel;

    public EntryCreatedNotificationHandler()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.QueueDeclare(
            queue: nameof(Queues.EntryCreatedQueue),
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }
    
    public Task Handle(EntryCreatedNotification notification, CancellationToken cancellationToken)
    {
        var message = JsonSerializer.Serialize(notification);
        var body = Encoding.UTF8.GetBytes(message);
        
        _channel.BasicPublish(
            exchange: "",
            routingKey: nameof(Queues.EntryCreatedQueue),
            basicProperties: null,
            body
        );

        return Task.CompletedTask;
    }
}