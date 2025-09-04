using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Services.Exceptions_Implementation;
using Shared.IdentityDtos;

namespace Services
{
    public class AuthenticationService(UserManager<AppUser> userManager, IConfiguration configuration) : IAuthenticationServices
    {
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
