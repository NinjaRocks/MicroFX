using System.Threading.Tasks;
using JustSaying.Messaging.MessageHandling;
using MicroFx.Bus.Subscriber;

namespace MicroFx.Bus.Aws
{
    public abstract class CommandHandler<T> : ICommandHandler<T>, IHandlerAsync<T>
        where T: Command
    {
        public abstract Task<bool> Handle(T message);
    }
}