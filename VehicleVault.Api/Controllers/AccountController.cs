using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Application.Features.UserFeature.DTOs;
using VehicleVault.Application.Features.UserFeature.Queries.Requests;
using VehicleVault.Application.Models;

namespace VehicleVault.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AccountController : AppControllerBase
    {
        [AllowAnonymous]
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
        
        [AllowAnonymous]
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

        [HttpPost]
        [Route("generate-new-jwt-token")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenModel tokenModel)
        {
            if (ModelState.IsValid) return BadRequest();

            var result = await Mediator.Send(new GenerateNewAccessTokenRequest(tokenModel));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("users/list")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await Mediator.Send(new GetAllUsersRequest());
            
            return NewResult(result);
        }
        
        [HttpGet]
        [Route("users/role={role}")]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetUsersByRoleRequest(role));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("users/id={userId}")]
        public async Task<IActionResult> GetUsersDetailsById(string userId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetUserDetailsByIdRequest(userId));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("users/email={userEmail}")]
        public async Task<IActionResult> GetUsersDetailsByEmail(string userEmail)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetUserDetailsByEmailRequest(userEmail));

            return NewResult(result);
        }
        
    }
}
