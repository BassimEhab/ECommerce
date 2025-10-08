using DomainLayer.Models.ProductModule;
using Shared;

namespace Service.Specification
{
    internal class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams)
            : base(
                p =>
                (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                &&
                (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                &&
                (string.IsNullOrEmpty(queryParams.search) || p.Name.ToLower().Contains(queryParams.search.ToLower()))
                )
        {
        }
    }
}
