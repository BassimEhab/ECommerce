using Shared;
using Shared.DataTransferObjects;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? BrandId, int? TypeId, ProductSortingOptions SortingOptions);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
    }
}
