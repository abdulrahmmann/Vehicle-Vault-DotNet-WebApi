using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Application.Features.UserFeature.DTOs;

namespace VehicleVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : AppControllerBase
    {
        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new RegisterUserRequest(registerUserDto));

            return NewResult(result);
        }
        
        [HttpPost]
        [Route("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new LoginUserRequest(loginUserDto));

            return NewResult(result);
        }
    }
}
