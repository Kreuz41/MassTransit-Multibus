using System.Reflection;
using MassTransit;
using MasstransitMultibus;
using MasstransitMultibus.Attributes;
using MasstransitMultibus.BusConfigurator;

var builder = WebApplication.CreateBuilder(args);

var sections = builder.Configuration.GetSection("RabbitMQ");
var rabbitHosts = builder.Configuration.GetSection("RabbitMQHosts").Get<Dictionary<string, RabbitConfigurationModel>>();

var busInterfaces = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.IsInterface && typeof(IBus).IsAssignableFrom(t) && t.GetCustomAttribute<BusAttribute>() != null);

if (rabbitHosts is not null)
    foreach (var host in rabbitHosts)
    {
        var bus = busInterfaces?.FirstOrDefault(b => b.GetCustomAttribute<BusAttribute>()?.Name == host.Key);
        if (bus is null)
            throw new Exception($"Bus for {host.Key} not found");
        
        var configuratorType = typeof(BusConfigurator<>).MakeGenericType(bus);
        var configurator = Activator.CreateInstance(configuratorType, host.Value);
        configuratorType.GetMethod("Configure")?.Invoke(configurator, [builder.Services]);
    }

builder.Services.AddHostedService<Service1>();
builder.Services.AddHostedService<Service2>();

var app = builder.Build();

app.Run();