using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrderDtos;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrders(OrderDto orderDto)
        {
            var Order = await _serviceManager.orderService.CreateOrder(orderDto, GetEmailFromToken());
            return Ok(Order);
        }
        // Get Delivery Methods
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _serviceManager.orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
        // Get All Order By Email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var orders = await _serviceManager.orderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(orders);
        }
        // Get Order By Id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var order = await _serviceManager.orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

    }
}
