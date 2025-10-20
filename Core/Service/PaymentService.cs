using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Service.Specification.OrderModuleSpecifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDtos;
using Stripe;
using Order = DomainLayer.Models.OrderModule.Order;
using Product = DomainLayer.Models.ProductModule.Product;
namespace Service
{
    public class PaymentService(IConfiguration _configuration,
                               IBasketRepository _basketRepository,
                               IUnitOfWork _unitOfWork,
                               IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            // Config Strip : Install package (Stripe.Net)
            StripeConfiguration.ApiKey = _configuration["StripSettings:SecretKey"];
            // Get Basket by BasketId 
            var Basket = await _basketRepository.GetBasketAsync(BasketId) ?? throw new BasketNotFoundException(BasketId);
            // Get Amount => Get Product + DeliveryMethod Cost
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }
            ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.deliveryMethodId.Value) ?? throw new DeliveryMethodNotFoundException(Basket.deliveryMethodId.Value);
            Basket.shippingPrice = DeliveryMethod.Price;
            var BasketAmount = (long)(Basket.Items.Sum(item => item.Quantity * item.Price) + DeliveryMethod.Price) * 100;
            // Create Payment Intent (Create Or Update)
            var PaymentService = new PaymentIntentService();
            if (Basket.paymentIntentId is null) // Create
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent = await PaymentService.CreateAsync(Options);
                Basket.paymentIntentId = PaymentIntent.Id;
                Basket.clientSecret = PaymentIntent.ClientSecret;
            }
            else
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = BasketAmount
                };
                await PaymentService.UpdateAsync(Basket.paymentIntentId, Options);
            }
            await _basketRepository.CreateOrUpdateBasketAync(Basket);
            return _mapper.Map<BasketDto>(Basket);
        }

        public async Task UpdatePaymentStatus(string request, string stripHeader)
        {

            var EndPointSecret = _configuration["StripSettings:EndPointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(request,
            stripHeader, EndPointSecret, throwOnApiVersionMismatch: false);
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            // Handle the event
            // If on SDK version < 46, use class Events instead of EventTypes
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentStatusSuccesseded(paymentIntent.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentStatusFailed(paymentIntent.Id);
            }
            // ... handle other event types
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
        }

        private async Task UpdatePaymentStatusSuccesseded(string paymentIntentId)
        {
            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await OrderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));
            order.status = OrderStatus.PaymentFailed;
            OrderRepo.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentStatusFailed(string paymentIntentId)
        {
            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await OrderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));
            order.status = OrderStatus.PaymentReceived;
            OrderRepo.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
