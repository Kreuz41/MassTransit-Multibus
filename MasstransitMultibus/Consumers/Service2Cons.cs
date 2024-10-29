using MassTransit;

namespace MasstransitMultibus.Consumers;

public class Service2Cons : IConsumer<Message>
{
    private readonly ILogger<Service2Cons> _logger;

    public Service2Cons(ILogger<Service2Cons> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<Message> context)
    {
        _logger.LogWarning($"Consumer 2 {context.Message.Value}");
        return Task.CompletedTask;
    }
}