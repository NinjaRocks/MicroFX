namespace MicroFx.ServiceBus
{
    public interface IMessagePublisher<in T> where T:IMessage
    {
        bool Publish(T message);
    }
}