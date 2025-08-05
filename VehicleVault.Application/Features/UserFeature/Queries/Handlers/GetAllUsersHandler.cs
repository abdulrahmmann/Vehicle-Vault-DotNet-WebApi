using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Constants;
using VehicleVault.Application.Features.UserFeature.DTOs;
using VehicleVault.Application.Features.UserFeature.Queries.Requests;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Features.UserFeature.Queries.Handlers;

public class GetAllUsersHandler: IRequestHandler<GetAllUsersRequest, UserResponse<IEnumerable<GetUserDto>>>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<GetAllUsersHandler>  _logger;
    #endregion
    
    
    #region Constructor
    public GetAllUsersHandler(UserManager<ApplicationUser> userManager, ILogger<GetAllUsersHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<UserResponse<IEnumerable<GetUserDto>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.User);

            if (users == null || !users.Any())
            {
                _logger.LogWarning("No users found in role: {Role}", Roles.User);
                return UserResponse<IEnumerable<GetUserDto>>
                    .Failure($"No users found in role: {Roles.User}", HttpStatusCode.NotFound);
            }

            var mappedUsers = users.Select(u => new GetUserDto(
                Id:u.Id.ToString(),
                UserName:u.UserName,
                Email:u.Email,
                PhoneNumber: u.PhoneNumber
                ));
            
            var usersList = mappedUsers.ToList();
            
            return UserResponse<IEnumerable<GetUserDto>>
                .Success(usersList.Count(), HttpStatusCode.OK, "Users retrieved successfully.", usersList);
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while Retrieving Users");
            return UserResponse<IEnumerable<GetUserDto>>
                .Failure("Unexpected server error. Please try again later.", HttpStatusCode.InternalServerError);
        }
    }
}