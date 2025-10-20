using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager) : ApiBaseController
    {
        [Authorize]
        [HttpPost("{BasketId}")] //POST BaseUrl/Api/Payments/adf234vs52gw520efdsf7
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket = await _serviceManager.paymentService.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(Basket);
        }
        const string endpointSecret = "whsec_...";

        [HttpPost("WebHook")]
        public async Task<ActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var StripHeaders = Request.Headers["Stripe-Signature"];
            //await _serviceManager.paymentService.UpdatePaymentStatus(json, StripHeaders);
            return new EmptyResult();
        }
    }
}
