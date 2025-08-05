using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Features.UserFeature.Commands.Handler;

public class CreateUserByAdminHandler: IRequestHandler<CreateUserByAdminRequest, UserResponse<Unit>>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<CreateUserByAdminHandler> _logger;
    #endregion

    #region Constructor
    public CreateUserByAdminHandler(UserManager<ApplicationUser> userManager, ILogger<CreateUserByAdminHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    #endregion

    public async Task<UserResponse<Unit>> Handle(CreateUserByAdminRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userRequest = request.UserDtoByAdmin;

            if (userRequest == null)
            {
                return UserResponse<Unit>
                    .Failure("Request is null", HttpStatusCode.BadRequest);
            }
            
            var mappedUser = new ApplicationUser
            {
                UserName = userRequest.UserName,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(mappedUser, userRequest.Password);

            if (!result.Succeeded)
            {
                return UserResponse<Unit>.Failure(result.Errors.First().Description, HttpStatusCode.BadRequest);
            }

            await _userManager.AddToRoleAsync(mappedUser, userRequest.Role ?? "USER");

            return UserResponse<Unit>.Created(mappedUser.Email, $"User With Email {mappedUser.Email} Created Successfully");
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to add user with email: {Email}", request.UserDtoByAdmin.Email);
            return UserResponse<Unit>
                .Failure($"Failed to add user with email: {request.UserDtoByAdmin.Email}", HttpStatusCode.BadRequest);
        }
    }
}