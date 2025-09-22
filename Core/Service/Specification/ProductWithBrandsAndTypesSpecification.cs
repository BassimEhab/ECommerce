using DomainLayer.Models;

namespace Service.Specification
{
    internal class ProductWithBrandsAndTypesSpecification : BaseSpecifications<Product, int>
    {
        // Get All Products With Types and Brands
        public ProductWithBrandsAndTypesSpecification() : base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        // Get Product By Id With Types and Brands
        public ProductWithBrandsAndTypesSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
