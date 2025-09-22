using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] // BaseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        // Get all Products
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(int? BrandId, int? TypeId)
        {
            var products = await _serviceManager.productService.GetAllProductsAsync(BrandId, TypeId);
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
