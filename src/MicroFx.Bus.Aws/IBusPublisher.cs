using JustSaying;
using MicroFx.Bus.Publisher;

namespace MicroFx.Bus.Aws
{
    public interface IBusPublisher: IPublisher
    {
        void WithBus(IMayWantOptionalSettings bus);
    }
}