namespace DomainLayer.Models.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; }// GUID : Created by the client
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
