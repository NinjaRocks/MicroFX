using System.Threading.Tasks;
using JustSaying.Messaging.MessageHandling;
using MicroFx.Bus.Subscriber;

namespace MicroFx.Bus.Aws
{
    public abstract class EventHandler<T> : IEventHandler<T>, IHandlerAsync<T>
        where T: Event
    {
        public abstract Task<bool> Handle(T message);
    }
}