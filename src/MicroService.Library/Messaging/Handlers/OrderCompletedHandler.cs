using System.Threading.Tasks;
using MicroFx.Bus.Aws;
using MicroService.Library.Messaging.Contracts;

namespace MicroService.Library.Messaging.Handlers
{
    public class OrderCompletedHandler : EventHandler<OrderCompleted>
    {
        public override Task<bool> Handle(OrderCompleted message)
        {
            // Some logic here ...
            // e.g. bll.NotifyRestaurantAboutOrder(message.OrderId);
            return Task.FromResult(true);
        }
    }
}