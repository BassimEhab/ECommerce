using DomainLayer.Models.OrderModule;

namespace Service.Specification.OrderModuleSpecifications
{
    internal class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId) : base(o => o.PaymentIntentId == PaymentIntentId)
        {
        }
    }
}
