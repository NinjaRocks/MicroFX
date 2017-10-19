using System.Threading.Tasks;

namespace MicroFx.Bus.Subscriber
{
    public interface ICommandHandler<T>: IMessageHandler
    {
        Task<bool> Handle(T message);
    }
}