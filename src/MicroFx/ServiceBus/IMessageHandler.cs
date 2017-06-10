namespace MicroFx.ServiceBus
{
    public interface IMessageHandler<in T> where T:IMessage
    {
        bool Handle(T message);
    }
}