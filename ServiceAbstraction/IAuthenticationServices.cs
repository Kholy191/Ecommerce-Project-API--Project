using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.IdentityDtos;

namespace ServiceAbstraction
{
    public interface IAuthenticationServices
    {
        public Task<UserDto> Login(LoginDto loginObj);
        public Task<UserDto> Register(RegisterDto registerObj);

    }
}
