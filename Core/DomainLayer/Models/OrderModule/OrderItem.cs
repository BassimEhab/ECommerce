﻿namespace DomainLayer.Models.OrderModule
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrder Product { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
