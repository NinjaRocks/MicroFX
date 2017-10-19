namespace MicroFx.Bus.Subscriber
{
    public interface ISubscriber: IRegisterCommandHandlers, IRegisterEventHandlers
    {
        void Listen();
        void Stop();
    }
}