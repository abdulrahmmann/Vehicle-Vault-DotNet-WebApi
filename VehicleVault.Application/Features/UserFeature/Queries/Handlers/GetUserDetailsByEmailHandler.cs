using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.DTOs;
using VehicleVault.Application.Features.UserFeature.Queries.Requests;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Features.UserFeature.Queries.Handlers;

public class GetUserDetailsByEmailHandler: IRequestHandler<GetUserDetailsByEmailRequest, UserResponse<GetUserDetailsDto>>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<GetUserDetailsByEmailHandler>  _logger;
    #endregion


    #region Constructor
    public GetUserDetailsByEmailHandler(UserManager<ApplicationUser> userManager, ILogger<GetUserDetailsByEmailHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    #endregion
    
    public async Task<UserResponse<GetUserDetailsDto>> Handle(GetUserDetailsByEmailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userEmail = request.Email;

            if (userEmail == null)
            {
                _logger.LogWarning("User Email is null : {Email}", userEmail);
                return UserResponse<GetUserDetailsDto>.Failure("User Email is not found", HttpStatusCode.NotFound);
            }

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                _logger.LogWarning("User Email Not Found : {Email}", userEmail);
                return UserResponse<GetUserDetailsDto>.Failure("User Email is null", HttpStatusCode.BadRequest);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var mappedUser = new GetUserDetailsDto(
                Id: user.Id.ToString(),
                UserName: user.UserName!,
                Email: user.Email,
                PhoneNumber: user.PhoneNumber,
                Role:role,
                RefreshToken:user.RefreshToken,
                RefreshTokenExpiration:user.RefreshTokenExpiration
                );
            
            return UserResponse<GetUserDetailsDto>
                .Success(1, HttpStatusCode.OK, "Users retrieved successfully.", mappedUser);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while Retrieving Users by Email: {Email}", request.Email);
            return UserResponse<GetUserDetailsDto>
                .Failure("Unexpected server error. Please try again later.", HttpStatusCode.InternalServerError);
        }
    }
}