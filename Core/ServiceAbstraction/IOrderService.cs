using Shared.DataTransferObjects.OrderDtos;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string email);
        // Get Delivery Methods
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
        // Get All Orders
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);
        // Get Order By Id
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);

    }
}
