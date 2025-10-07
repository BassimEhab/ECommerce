using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductDtos;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Get all Products
        //[Authorize(Roles = "Admin")]
        [HttpGet()]
        [Cache]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            var products = await _serviceManager.productService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }
        // Get Product By Id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _serviceManager.productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        // Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManager.productService.GetAllTypesAsync();
            return Ok(types);
        }
        // Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _serviceManager.productService.GetBrandsAsync();
            return Ok(brands);
        }
    }
}
