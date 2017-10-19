using MicroFx.Bus;
using MicroFx.Bus.Aws;

namespace MicroService.Library.Messaging.Contracts
{
    [MessageQueue("OrderCompleted")]
    public class OrderCompleted : Event
    {
        public OrderCompleted(int orderId)
        {
            OrderId = orderId;
        }
        public int OrderId { get; private set; }
    }
}