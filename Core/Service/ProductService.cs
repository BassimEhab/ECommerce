using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DataTransferObjects;
namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var products = await repo.GetAllAsync();
            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return productsDto;
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetByIdAsync(id);
            var productDto = _mapper.Map<Product, ProductDto>(product);
            return productDto;

        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var types = await repo.GetAllAsync();
            var typesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
            return typesDto;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
            var brandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandsDto;
        }

    }
}
