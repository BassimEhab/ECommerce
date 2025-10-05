namespace DomainLayer.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int id) : NotFoundException($"No Delivery Method Found With id = {id}")
    {
    }
}
