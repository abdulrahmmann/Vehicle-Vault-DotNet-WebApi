using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.DTOs;
using VehicleVault.Application.Features.UserFeature.Queries.Requests;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Features.UserFeature.Queries.Handlers;

public class GetUserDetailsByIdHandler: IRequestHandler<GetUserDetailsByIdRequest, UserResponse<GetUserDetailsDto>>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<GetUserDetailsByIdHandler>  _logger;
    #endregion


    #region Constructor
    public GetUserDetailsByIdHandler(UserManager<ApplicationUser> userManager, ILogger<GetUserDetailsByIdHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    #endregion
    
    public async Task<UserResponse<GetUserDetailsDto>> Handle(GetUserDetailsByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = request.Id;

            if (Convert.ToInt16(userId) <= 0)
            {
                _logger.LogWarning("User Id < 0 : {Id}", userId);
                return UserResponse<GetUserDetailsDto>.Failure("user Id < 0", HttpStatusCode.UnprocessableEntity);
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning("User Equal Null : {Id}", userId);
                return UserResponse<GetUserDetailsDto>.Failure("User Equal Null", HttpStatusCode.BadRequest);
            }
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var mappedUser = new GetUserDetailsDto(
                Id: user.Id.ToString(),
                UserName: user.UserName!,
                Email: user.Email,
                PhoneNumber: user.PhoneNumber,
                RefreshToken:user.RefreshToken,
                RefreshTokenExpiration:user.RefreshTokenExpiration
            );
            
            return UserResponse<GetUserDetailsDto>
                .Success(1, HttpStatusCode.OK, "Users retrieved successfully.", mappedUser);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while Retrieving Users by Id: {Id}", request.Id);
            return UserResponse<GetUserDetailsDto>
                .Failure("Unexpected server error. Please try again later.", HttpStatusCode.InternalServerError);
        }
    }
}