namespace MicroFx
{
    public interface IServiceSettings
    {
        int? Port { get; set; }
        string Host { get; set; }
        string ServiceName { get; set; }
        string Description { get; set; }
    }
}