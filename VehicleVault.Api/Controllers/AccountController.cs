using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Application.Features.UserFeature.DTOs;
using VehicleVault.Application.Models;
using VehicleVault.Application.Services.Tokens.GeneratePrincipalJwtToken;
using VehicleVault.Application.Services.Tokens.GenerateToken;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : AppControllerBase
    {
        private readonly IGeneratePrincipalFromJwtTokenService _jwtTokenService;
        private readonly IGenerateTokenService  _generateTokenService;
        private readonly UserManager<ApplicationUser>  _userManager;

        public AccountController(IGeneratePrincipalFromJwtTokenService jwtTokenService, UserManager<ApplicationUser> userManager, IGenerateTokenService generateTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _generateTokenService = generateTokenService;
        }

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

        [HttpPost]
        [Route("generate-new-jwt-token")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenModel tokenModel)
        {
            if (ModelState.IsValid) return BadRequest();

            var result = await Mediator.Send(new GenerateNewAccessTokenRequest(tokenModel));

            return NewResult(result);
        }
    }
}
