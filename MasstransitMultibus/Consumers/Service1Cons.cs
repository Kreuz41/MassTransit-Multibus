using MassTransit;
using MasstransitMultibus.Attributes;

namespace MasstransitMultibus.Consumers;

public class Service1Cons : IConsumer<Message>
{
    private readonly ILogger<Service1Cons> _logger;

    public Service1Cons(ILogger<Service1Cons> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<Message> context)
    {
        _logger.LogInformation($"Consumer 1 {context.Message.Value}");
        return Task.CompletedTask;
    }
}