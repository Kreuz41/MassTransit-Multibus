using System.Reflection;
using MassTransit;
using MasstransitMultibus.Attributes;
using MasstransitMultibus.Consumers;

namespace MasstransitMultibus.BusConfigurator;

public class BusConfigurator<TBus> : IBusConfigurator<TBus>
    where TBus : class, IBus
{
    private readonly RabbitConfigurationModel _configuration;
    
    public BusConfigurator(RabbitConfigurationModel configuration)
    {
        _configuration = configuration;
        
        var hostAttribute = typeof(TBus).GetCustomAttribute<BusAttribute>();
        if (hostAttribute is null)
            throw new ArgumentException("Bus interface missing BusAttribute");
    }
    
    public void Configure(IServiceCollection services)
    {
        services.AddMassTransit<TBus>(x =>
        {
            x.AddConsumers(Assembly.GetExecutingAssembly());
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(
                    host: _configuration.Host, 
                    port: _configuration.Port, 
                    virtualHost: _configuration.VirtualHost, 
                    configure: h =>
                    {
                        h.Username(_configuration.Username);
                        h.Password(_configuration.Password);
                    });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}