using MassTransit;
using MassTransit.DependencyInjection;
using MasstransitMultibus.Buses;

namespace MasstransitMultibus;

public class Service2 : BackgroundService
{
    private readonly IServiceScopeFactory _factory;

    public Service2(IServiceScopeFactory factory)
    {
        _factory = factory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var i = 0;
        var scope = _factory.CreateScope();
        var endpoint = scope.ServiceProvider.GetRequiredService<Bind<ISecondBus, IPublishEndpoint>>();
        while (true)
        {
            await endpoint.Value.Publish(new Message { Value = $"{nameof(Service2)}\nMessage: {i}"}, stoppingToken);
            i++;
            await Task.Delay(1000, stoppingToken);
        }
        
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}