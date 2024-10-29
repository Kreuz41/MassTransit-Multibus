using MassTransit;

namespace MasstransitMultibus.BusConfigurator;

public interface IBusConfigurator<TBus>
{
    void Configure(IServiceCollection services);
}