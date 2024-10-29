using MassTransit;
using MasstransitMultibus.Attributes;

namespace MasstransitMultibus.Buses;

[Bus("Second")]
public interface ISecondBus : IBus
{ }