namespace DomainLayer.Exceptions
{
    public sealed class AddressNotFoundException(string userName) : NotFoundException($"Address Not Found For {userName}")
    {
    }
}
