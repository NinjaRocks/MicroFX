using MicroFx.Bus.Aws;

namespace MicroService.Library.Messaging.Contracts
{
    public class OrderAccepted : Command
    {
        public OrderAccepted(int orderId)
        {
            OrderId = orderId;
        }
        public int OrderId { get; private set; }
    }
}
