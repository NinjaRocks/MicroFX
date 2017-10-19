namespace MicroFx.Bus
{
    public interface IServiceBus
    {
        void Initialise();
        void ConfigurePointToPoint();
        void ConfigurePubSub();
        void Start();
        void Stop();
    }
}