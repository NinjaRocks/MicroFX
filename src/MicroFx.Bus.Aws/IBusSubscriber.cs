using JustSaying;
using MicroFx.Bus.Subscriber;

namespace MicroFx.Bus.Aws
{
    public interface IBusSubscriber : ISubscriber
    {
        void WithBus(IMayWantOptionalSettings bus);
    }
}