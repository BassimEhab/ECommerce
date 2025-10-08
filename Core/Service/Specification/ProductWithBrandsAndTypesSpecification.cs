using DomainLayer.Models.ProductModule;
using Shared;

namespace Service.Specification
{
    internal class ProductWithBrandsAndTypesSpecification : BaseSpecifications<Product, int>
    {
        // Get All Products With Types and Brands
        public ProductWithBrandsAndTypesSpecification(ProductQueryParams queryParams)
            // Send Criteria value
            : base(
                p =>
                (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                &&
                (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                &&
                (string.IsNullOrEmpty(queryParams.search) || p.Name.ToLower().Contains(queryParams.search.ToLower()))
                )
        {
            // Send Includes Values
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            // Send Sorting Option Value
            switch (queryParams.sort)
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
            // Apply Pagination
            ApplyPagination(queryParams.PageSize, queryParams.pageNumber);
        }
        // Get Product By Id With Types and Brands
        public ProductWithBrandsAndTypesSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
