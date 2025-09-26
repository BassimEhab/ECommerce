using AutoMapper;
using DomainLayer.Models.BasketModule;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Service.MappingProfiles
{
    class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
