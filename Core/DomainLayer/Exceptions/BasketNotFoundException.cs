namespace DomainLayer.Exceptions
{
    public sealed class BasketNotFoundException(string Id) : NotFoundException($"Basket With Id : {Id} Is Not Found!")
    {
    }
}
