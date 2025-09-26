using Shared.DataTransferObjects.BasketModuleDtos;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        // Get, Create Or Update And Delete
        Task<BasketDto> GetBasketAsync(string Key);
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto Basket);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
