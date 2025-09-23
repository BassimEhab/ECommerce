using DomainLayer.Models;
using Shared;

namespace Service.Specification
{
    internal class ProductWithBrandsAndTypesSpecification : BaseSpecifications<Product, int>
    {
        // Get All Products With Types and Brands
        public ProductWithBrandsAndTypesSpecification(ProductQueryParams queryParams)
            : base(
                p =>
                (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                &&
                (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                )
        {

            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (queryParams.SortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
        }
        // Get Product By Id With Types and Brands
        public ProductWithBrandsAndTypesSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
