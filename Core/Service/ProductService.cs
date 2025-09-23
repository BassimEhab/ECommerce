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
        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandsAndTypesSpecification(queryParams);
            var products = await Repo.GetAllAsync(specification);
            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var ProductsCount = products.Count();
            var TotalCount = await Repo.CountAsync(new ProductCountSpecification(queryParams));
            return new PaginatedResult<ProductDto>(queryParams.PageIndex, ProductsCount, TotalCount, productsDto);
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
