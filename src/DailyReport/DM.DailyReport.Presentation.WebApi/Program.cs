using DM.Application.Behaviors;
using DM.Infrastructure;
using DM.Presentation.WebApi.Middlewares;
using FluentValidation;
using MediatR;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//Build infrastructure
builder.Services.AddInfrastructure(configuration);

//Buid Application layer
var applicationAssembly = typeof(DM.Application.AssemblyReference).Assembly;
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(applicationAssembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(applicationAssembly);

//Buid Presentation layer
var presentationAssembly = typeof(DM.Presentation.AssemblyReference).Assembly;
builder.Services
    .AddControllersWithViews()
    .AddApplicationPart(presentationAssembly);


//Build Notification hub (rabbitMQ)
builder.Services.AddSingleton<IConnectionFactory>(
    sp => new ConnectionFactory()
    {
        HostName = configuration["MQ:HostName"],
        Port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672"),
        UserName = configuration["MQ:UserName"],
        Password = configuration["MQ:Password"]
    }
);
builder.Services.AddSingleton<IConnection>(sp => sp
    .GetRequiredService<IConnectionFactory>()
    .CreateConnection()
);

builder.Services.AddSingleton<IModel>(sp =>
{
    var connection = sp.GetRequiredService<IConnection>();
    return connection.CreateModel();
});

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(applicationAssembly));

//Build OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

//Build APP
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.Run();