using MassTransit;
using MasstransitMultibus.Attributes;

namespace MasstransitMultibus.Buses;

[Bus("First")]
public interface IFirstBus : IBus
{ }