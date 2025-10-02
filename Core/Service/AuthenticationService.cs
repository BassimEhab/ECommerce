using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, IMapper _mapper) : IAuthenticationService
    {


        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
            var IsPasswordCorrect = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordCorrect)
            {
                return new UserDto() { DisplayName = User.DisplayName, Email = User.Email, Token = await CreateTokenAsync(User) };
            }
            else
                throw new UnauthorizedException();

        }
        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName
            };
            var Result = await _userManager.CreateAsync(User, registerDto.Password);
            if (Result.Succeeded)
                return new UserDto() { DisplayName = User.DisplayName, Email = User.Email, Token = await CreateTokenAsync(User) };
            else
            {
                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.NameIdentifier, user.Id!)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
                Claims.Add(new Claim(ClaimTypes.Role, role));
            var SecertKey = _configuration.GetSection("JwtOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecertKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuration["JwtOptions:Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(7),
                signingCredentials: Creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        public async Task<bool> CheckEmailAync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            return User is not null;
        }
        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto() { DisplayName = User.DisplayName, Email = User.Email, Token = await CreateTokenAsync(User) };
        }
        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var User = await _userManager.Users.Include(a => a.Address).FirstOrDefaultAsync(e => e.Email == email) ?? throw new UserNotFoundException(email);
            if (User.Address is not null)
                return _mapper.Map<Address, AddressDto>(User.Address);
            else
                throw new AddressNotFoundException(User.UserName);
        }
        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var User = await _userManager.Users.Include(a => a.Address).FirstOrDefaultAsync(e => e.Email == email) ?? throw new UserNotFoundException(email);
            if (User.Address is not null)
            {
                User.Address.FirstName = addressDto.FirstName ?? User.Address.FirstName;
                User.Address.LastName = addressDto.LastName ?? User.Address.LastName;
                User.Address.Street = addressDto.Street ?? User.Address.Street;
                User.Address.City = addressDto.City ?? User.Address.City;
                User.Address.Country = addressDto.Country ?? User.Address.Country;
            }
            else // Add New Address
            {
                User.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            await _userManager.UpdateAsync(User);
            return _mapper.Map<Address, AddressDto>(User.Address);
        }

    }
}
