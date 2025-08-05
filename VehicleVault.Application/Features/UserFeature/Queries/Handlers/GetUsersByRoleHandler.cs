using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.DTOs;
using VehicleVault.Application.Features.UserFeature.Queries.Requests;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Features.UserFeature.Queries.Handlers;

public class GetUsersByRoleHandler: IRequestHandler<GetUsersByRoleRequest, UserResponse<IEnumerable<GetUserDto>>>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser>  _userManager;
    private readonly ILogger<GetUsersByRoleHandler> _logger;
    #endregion
    
    #region Constructor
    public GetUsersByRoleHandler(UserManager<ApplicationUser> userManager, ILogger<GetUsersByRoleHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<UserResponse<IEnumerable<GetUserDto>>> Handle(GetUsersByRoleRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userRequest = request.Role;

            if (userRequest == null)
            {
                return UserResponse<IEnumerable<GetUserDto>>
                    .Failure($"Role input is null: {userRequest}", HttpStatusCode.NotFound);
            }

            if (userRequest.ToUpper() != "ADMIN" && userRequest.ToUpper() != "USER")
            {
                return UserResponse<IEnumerable<GetUserDto>>
                    .Failure($"Role Request Input Validation Error: {userRequest}", HttpStatusCode.NotFound); 
            }
            var users = await _userManager.GetUsersInRoleAsync(userRequest.ToUpper());

            if (users == null || !users.Any())
            {
                return UserResponse<IEnumerable<GetUserDto>>
                    .Failure("No users found with the specified role.", HttpStatusCode.NotFound);
            }
            
            var mappedUsers = users.Select(u => new GetUserDto(
                Id:u.Id, 
                UserName:u.UserName,
                Email:u.Email, 
                PhoneNumber:u.PhoneNumber
                ));
            
            return UserResponse<IEnumerable<GetUserDto>>
                .Success(mappedUsers.Count(), HttpStatusCode.OK, "Users retrieved successfully.", mappedUsers);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while Retrieving Users by Role: {Role}", request.Role);
            return UserResponse<IEnumerable<GetUserDto>>
                .Failure("Unexpected server error. Please try again later.", HttpStatusCode.InternalServerError);
        }
    }
}