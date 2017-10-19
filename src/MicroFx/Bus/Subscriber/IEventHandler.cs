using System.Threading.Tasks;

namespace MicroFx.Bus.Subscriber
{
    public interface IEventHandler<T> : IMessageHandler
    {
        Task<bool> Handle(T message);
    }
}