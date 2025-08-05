using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Common;

namespace VehicleVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppControllerBase : ControllerBase
    {
        #region Mediator Instance
        
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
        
        #endregion
        
        
        #region Standardized Response Handler
        protected ObjectResult NewResult<T>(BaseResponse<T> response)
        {
            return response.HttpStatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.Created => Created(string.Empty, response),
                HttpStatusCode.Unauthorized => Unauthorized(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                HttpStatusCode.NotFound => NotFound(response),
                HttpStatusCode.Accepted => Accepted(response),
                HttpStatusCode.UnprocessableEntity => UnprocessableEntity(response),
                HttpStatusCode.Conflict => Conflict(response),
                _ => StatusCode((int)response.HttpStatusCode, response)
            };
        }
        
        // 🔁 Overload for AuthenticationResponse
        protected ObjectResult NewResult(AuthenticationResponse response)
        {
            return response.HttpStatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.Created => Created(string.Empty, response),
                HttpStatusCode.Unauthorized => Unauthorized(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                HttpStatusCode.NotFound => NotFound(response),
                HttpStatusCode.Accepted => Accepted(response),
                HttpStatusCode.UnprocessableEntity => UnprocessableEntity(response),
                HttpStatusCode.Conflict => Conflict(response),
                _ => StatusCode((int)response.HttpStatusCode, response)
            };
        }
        
        // 🔁 Overload for UserResponse
        protected ObjectResult NewResult<T>(UserResponse<T> response)
        {
            return response.HttpStatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.Created => Created(string.Empty, response),
                HttpStatusCode.Unauthorized => Unauthorized(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                HttpStatusCode.NotFound => NotFound(response),
                HttpStatusCode.Accepted => Accepted(response),
                HttpStatusCode.UnprocessableEntity => UnprocessableEntity(response),
                HttpStatusCode.Conflict => Conflict(response),
                _ => StatusCode((int)response.HttpStatusCode, response)
            };
        }
        
        #endregion
    }
}
