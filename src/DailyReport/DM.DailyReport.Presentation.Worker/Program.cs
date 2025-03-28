using DM.Application.Behaviors;
using DM.DailyReport.Presentation.Worker;
using DM.DailyReport.Presentation.Worker.Workers;
using DM.Infrastructure;
using MediatR;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHostedService<Worker>();

//Build infrastructure
builder.Services.AddInfrastructure(configuration);

//Buid Application layer
var applicationAssembly = typeof(DM.Application.AssemblyReference).Assembly;
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(applicationAssembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = configuration["MQ:HostName"],
        Port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672"),
        UserName = configuration["MQ:UserName"],
        Password = configuration["MQ:Password"],
        DispatchConsumersAsync = true
    };
    return factory.CreateConnection();
});

builder.Services.AddSingleton<IModel>(sp => sp.GetRequiredService<IConnection>().CreateModel());
builder.Services.AddHostedService<RabbitMqWorker>();

var host = builder.Build();
host.Run();