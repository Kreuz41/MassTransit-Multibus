namespace MasstransitMultibus.Attributes;

public class BusAttribute : Attribute
{
    public string Name { get; private set; }

    public BusAttribute(string busName) => Name = busName;
}