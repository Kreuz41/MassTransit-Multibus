namespace MasstransitMultibus;

public class RabbitConfigurationModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public string VirtualHost { get; set; }
    public ushort Port { get; set; }
}