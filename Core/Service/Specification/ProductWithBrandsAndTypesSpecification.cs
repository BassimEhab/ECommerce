using DomainLayer.Models;
using Shared;

namespace Service.Specification
{
    internal class ProductWithBrandsAndTypesSpecification : BaseSpecifications<Product, int>
    {
        // Get All Products With Types and Brands
        public ProductWithBrandsAndTypesSpecification(int? BrandId, int? TypeId, ProductSortingOptions SortingOption)
            : base(
                p =>
                (!BrandId.HasValue || p.BrandId == BrandId)
                &&
                (!TypeId.HasValue || p.TypeId == TypeId)
                )
        {

            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (SortingOption)
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
