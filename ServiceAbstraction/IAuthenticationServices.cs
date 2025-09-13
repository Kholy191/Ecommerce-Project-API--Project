using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.AddressDtos;
using Shared.IdentityDtos;

namespace ServiceAbstraction
{
    public interface IAuthenticationServices
    {
        public Task<UserDto> Login(LoginDto loginObj);
        public Task<UserDto> Register(RegisterDto registerObj);

        public Task<bool> CheckEmailAsync(string email);

        public Task<AddressDto> GetCurrentAddressAsync(string email); 

        public Task<AddressDto> UpdateUserAddressAsync(string email, AddressDto address);

        public Task<UserDto> GetCurrentUserAsync();

    }
}
