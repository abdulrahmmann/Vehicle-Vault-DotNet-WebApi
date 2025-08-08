using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.BodyFeature.Commands.Requests;
using VehicleVault.Application.Features.BodyFeature.DTOs;
using VehicleVault.Application.Features.BodyFeature.Queries.Requests;

namespace VehicleVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class BodyController : AppControllerBase
    {
        #region GET
        [HttpGet]
        [Route("bodies-list")]
        public async Task<IActionResult> GetAllBodies()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetAllBodiesRequest());

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("by-id")]
        public async Task<IActionResult> GetBodyById([FromQuery] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetBodyByIdRequest(id));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("by-name")]
        public async Task<IActionResult> GetBodyByName([FromQuery] string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetBodyByNameRequest(name));

            return NewResult(result);
        }
        #endregion


        #region POST
        [HttpPost]
        [Route("create-body")]
        public async Task<IActionResult> CreateBody([FromBody] CreateBodyDto bodyDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new CreateBodyRequest(bodyDto));

            return NewResult(result);
        }
        
        [HttpPost]
        [Route("create-body-list")]
        public async Task<IActionResult> CreateBodyRange([FromBody] IEnumerable<CreateBodyDto> bodiesDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new CreateBodyRangeRequest(bodiesDto));

            return NewResult(result);
        }
        #endregion


        #region DELETE
        [HttpDelete]
        [Route("delete-body")]
        public async Task<IActionResult> SoftDeleteBody([FromQuery] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new SoftDeleteBodyRequest(id));

            return NewResult(result);
        }
        #endregion


        #region PUT & PATCH
        [HttpPut()]
        [Route("update-body")]
        public async Task<IActionResult> UpdateMake([FromQuery] int id, [FromBody] UpdateBodyDto bodyDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new UpdateBodyRequest(id, bodyDto));

            return NewResult(result);
        }
        
        [HttpPut()]
        [Route("restore-body")]
        public async Task<IActionResult> RestoreCategory([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than zero.");

            var result = await Mediator.Send(new RestoreBodyRequest(id));

            return NewResult(result);
        }
        #endregion
    }
}
