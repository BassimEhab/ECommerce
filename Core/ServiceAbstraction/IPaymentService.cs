using Shared.DataTransferObjects.BasketModuleDtos;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId);
        Task UpdatePaymentStatus(string request, string stripHeader);
    }
}
