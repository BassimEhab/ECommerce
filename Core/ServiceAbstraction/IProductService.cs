using Shared.DataTransferObjects;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
    }
}
