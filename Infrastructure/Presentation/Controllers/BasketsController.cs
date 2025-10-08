using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Presentation.Controllers
{
    public class BasketsController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Get Basket 
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var Basket = await _serviceManager.basketService.GetBasketAsync(key);
            return Ok(Basket);
        }
        // Create Or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        // Delete Basket
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Result = await _serviceManager.basketService.DeleteBasketAsync(key);
            return Ok(Result);
        }


    }
}
