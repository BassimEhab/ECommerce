using Shared.DataTransferObjects.IdentityDtos;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        // Login
        // Take Email And Password 
        // Return Token, Email And DisplayName
        Task<UserDto> LoginAsync(LoginDto loginDto);
        // Register
        // Take Email, Password, UserName, Phone Number And DisplayName
        // Return Token, Email And DisplayName
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        //Check Email
        // Take String: Email And Return boolean
        Task<bool> CheckEmailAync(string email);
        // Get Current User Address
        // Take String: Email And Retrn Address
        Task<AddressDto> GetCurrentUserAddressAsync(string email);
        // Update Current User Address
        // Take AddressDto: Updated Address And String: Email Then Return Address After Update
        Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto);
        // Get Current User
        // Take String: Email And Return Token, Email And Display Name
        Task<UserDto> GetCurrentUserAsync(string email);

    }
}
