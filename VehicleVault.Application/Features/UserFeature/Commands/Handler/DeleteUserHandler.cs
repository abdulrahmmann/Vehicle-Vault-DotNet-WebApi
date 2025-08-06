using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using VehicleVault.Application.Common;
using VehicleVault.Application.Features.UserFeature.Commands.Requests;
using VehicleVault.Domain.IdentityEntities;

namespace VehicleVault.Application.Features.UserFeature.Commands.Handler;

public class DeleteUserHandler: IRequestHandler<DeleteUserRequest, BaseResponse<Unit>>
{
    #region Instance Fields
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<DeleteUserHandler> _logger;
    #endregion

    #region Constructor
    public DeleteUserHandler(UserManager<ApplicationUser> userManager, ILogger<DeleteUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.UserEmail == null) return BaseResponse<Unit>.ValidationError("email request is error");

            var user = await _userManager.FindByEmailAsync(request.UserEmail);
            
            if (user == null) return BaseResponse<Unit>.NotFound("user not found");

            user.IsDeleted = true;
            
            await _userManager.UpdateAsync(user);
            
            return BaseResponse<Unit>.Success($"user with email: {request.UserEmail} deleted successfully");
        }
        catch (Exception e)
        {
            _logger.LogError("{error}", e.Message);
            return BaseResponse<Unit>.InternalError("Error deleting user");
        }
    }
}