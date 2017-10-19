using MicroFx.Bus.Configuration;

namespace MicroService.Library.Host
{
    [ServiceBusConfiguration]
    public class ServiceBusConfig: IBusConfiguration
    {
        public void Configure(IConfigContext context)
        {
            
        }
    }
}
