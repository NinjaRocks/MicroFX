namespace MicroFx.Bus.Configuration
{
    [ServiceBusConfiguration]
    public interface IBusConfiguration
    {
        void Configure(IConfigContext context);
    }
}