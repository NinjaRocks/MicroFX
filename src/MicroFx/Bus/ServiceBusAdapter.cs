namespace MicroFx.Bus
{
    public class ServiceBusAdapter: IServiceBus
    {
        private readonly IServiceBus bus;
        public ServiceBusAdapter(IServiceBus bus)
        {
            this.bus = bus;
        }

        public void Initialise()
        {
            bus.Initialise();
        }

        public void ConfigurePointToPoint()
        {
            bus.ConfigurePointToPoint();
        }
        public void ConfigurePubSub()
        {
            bus.ConfigurePubSub();
        }

        public void Start()
        {
            bus.Start();
        }

        public void Stop()
        {
            bus.Stop();
        }
    }
}