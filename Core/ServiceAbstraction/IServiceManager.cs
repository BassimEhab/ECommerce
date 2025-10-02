namespace ServiceAbstraction
{
    public interface IServiceManager
    {
        public IProductService productService { get; }
        public IBasketService basketService { get; }
        public IAuthenticationService authenticationService { get; }
    }
}
