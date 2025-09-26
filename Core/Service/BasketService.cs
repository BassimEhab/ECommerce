using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto Basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(Basket);
            var IsCreatedOrUpdated = await _basketRepository.CreateOrUpdateBasketAync(CustomerBasket);
            if (IsCreatedOrUpdated is not null)
                return await GetBasketAsync(Basket.Id);
            else
                throw new Exception("Can't Update Or Create Basket Now Tryagain Later!");
        }

        public async Task<bool> DeleteBasketAsync(string Key) => await _basketRepository.DeleteBasketAsync(Key);

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket = await _basketRepository.GetBasketAsync(Key);
            if (Basket is not null)
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            else
                throw new BasketNotFoundException(Key);

        }
    }
}
