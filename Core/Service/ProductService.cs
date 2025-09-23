using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specification;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? BrandId, int? TypeId, ProductSortingOptions SortingOption)
        {
            var specification = new ProductWithBrandsAndTypesSpecification(BrandId, TypeId, SortingOption);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specification);
            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return productsDto;
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithBrandsAndTypesSpecification(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specification);
            var productDto = _mapper.Map<Product, ProductDto>(product);
            return productDto;

        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
            return typesDto;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandsDto;
        }

    }
}
