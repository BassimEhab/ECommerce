using ServiceAbstraction;

namespace Service
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> ProductFactory,
                                                   Func<IBasketService> BasketFactory,
                                                   Func<IAuthenticationService> AuthenticationFactory,
                                                   Func<IOrderService> OrderFactory,
                                                   Func<IPaymentService> PaymentFactory) : IServiceManager
    {
        public IProductService productService => ProductFactory.Invoke();

        public IBasketService basketService => BasketFactory.Invoke();

        public IAuthenticationService authenticationService => AuthenticationFactory.Invoke();

        public IOrderService orderService => OrderFactory.Invoke();

        public IPaymentService paymentService => PaymentFactory.Invoke();
    }
}
