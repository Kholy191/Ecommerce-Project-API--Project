using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.IdentityDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await serviceManager.AuthenticationServices.Login(loginDto);
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto regDto)
        {
            var user = await serviceManager.AuthenticationServices.Register(regDto);
            return Ok(user);
        }
    }
}
