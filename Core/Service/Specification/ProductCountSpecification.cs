using DomainLayer.Models;
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
                (string.IsNullOrEmpty(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))
                )
        {
        }
    }
}
