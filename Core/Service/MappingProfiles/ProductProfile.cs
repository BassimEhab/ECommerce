using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects.ProductDtos;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(des => des.productBrand, options => options
                .MapFrom(src => src.ProductBrand.Name))

                .ForMember(des => des.productType, options => options
                .MapFrom(src => src.ProductType.Name))

                .ForMember(des => des.PictureUrl, options => options
                .MapFrom<PictureUrlResolver>());

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();
        }
    }
}
