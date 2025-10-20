namespace DomainLayer.Models.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public Order() { }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal, string paymentIntentId)
        {
            buyerEmail = userEmail;
            shipToAddress = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string buyerEmail { get; set; } = default!;
        public OrderAddress shipToAddress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public int DeliveryMethodId { get; set; } // Fk
        public OrderStatus status { get; set; }
        //[NotMapped]
        //public decimal Total { get => Subtotal + DeliveryMethod.Price; }

        public decimal GetTotal() => Subtotal + DeliveryMethod.Price;
        public string PaymentIntentId { get; set; } = default!;
    }
}
