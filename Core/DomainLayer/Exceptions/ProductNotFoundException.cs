namespace DomainLayer.Exceptions
{
    public sealed class ProductNotFoundException(int Id) : NotFoundException($"Product With Id : {Id} Is Not Found!")
    {
    }
}
