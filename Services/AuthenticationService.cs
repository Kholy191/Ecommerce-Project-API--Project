using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Services.Exceptions_Implementation;
using Shared.AddressDtos;
using Shared.IdentityDtos;

namespace Services
{
    public class AuthenticationService(UserManager<AppUser> userManager, IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IAuthenticationServices
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var User = await userManager.FindByEmailAsync(email);
            if (User == null) 
                return false;
            return true;
        }

        public async Task<AddressDto> GetCurrentAddressAsync(string email)
        {
            var User = await userManager.Users.Include(u => u.address).FirstOrDefaultAsync(u => u.Email == email);
            if (User == null)
                throw new userNotFoundException("There is no user by this email");
            if (User.address is null)
                throw new AddressNotFoundException();
            else
                return mapper.Map<AddressDto>(User.address);
        }

        public async Task<UserDto> GetCurrentUserAsync()
        {
            var email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new userNotFoundException("There is no user by this email");
            else
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = email,
                    Token = await TokenGenerator(user)
                };
        }

        public async Task<UserDto> Login(LoginDto loginObj)
        {
            var user = await userManager.FindByEmailAsync(loginObj.Email);
            if (user == null)
            {
                throw new userNotFoundException(loginObj.Email);
            }

            var IsValidPassword = await userManager.CheckPasswordAsync(user, loginObj.Password);
            if (!IsValidPassword)
                return new UserDto { Email = loginObj.Email, DisplayName = user.DisplayName, Token = await TokenGenerator(user) };
            else
                throw new NotAuthorizedException();
        }

        public async Task<UserDto> Register(RegisterDto registerObj)
        {
            var user = new AppUser()
            {
                DisplayName = registerObj.DisplayName,
                Email = registerObj.Email,
                PhoneNumber = registerObj.PhoneNumber,
                UserName = registerObj.UserName,
            };
            var Result = await userManager.CreateAsync(user, registerObj.Password);

            if (!Result.Succeeded)
            {
                var Errors = Result.Errors.Select(m => m.Description);
                throw new BadRequestException(Errors);
            }

            return new UserDto()
            {
                DisplayName = registerObj.DisplayName,
                Email = registerObj.Email,
                Token = await TokenGenerator(user)
            };
        }

        public async Task<AddressDto> UpdateUserAddressAsync(string email, AddressDto address)
        {
            var user = await userManager.Users.Include(u => u.address).FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new userNotFoundException("No User");
            }
            if(user.address == null)
            {
                var newAddress = mapper.Map<Address>(address);
                user.address = newAddress;
            }
            else
            {
                user.address.FirstName = address.FirstName;
                user.address.LastName = address.LastName;
                user.address.AppUserId = user.Id;
                user.address.City = address.City;
                user.address.Country = address.Country;
                user.address.Street = address.Street;
            }
            await userManager.UpdateAsync(user);
            return mapper.Map<AddressDto>(user.address);
            
        }

        private async Task<string> TokenGenerator(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
            };
            var roles = await userManager.GetRolesAsync(user);

            foreach (var item in roles)
                claims.Add(new Claim(ClaimTypes.Role, item));

            var SecretKey = configuration.GetSection("JwtConfig")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));

            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: configuration.GetSection("JwtConfig")["Issuer"],
                audience: configuration.GetSection("JwtConfig")["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: Creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
