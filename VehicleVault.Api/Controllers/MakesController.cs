using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.MakesFeature.Commands.Requests;
using VehicleVault.Application.Features.MakesFeature.DTOs;
using VehicleVault.Application.Features.MakesFeature.Queries.Requests;

namespace VehicleVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class MakesController : AppControllerBase
    {
        #region GET

        [HttpGet]
        [Route("makes-list")]
        public async Task<IActionResult> GerAllMakes()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetAllMakesRequest());

            return NewResult(result);
        }
        
        [HttpGet("make-id")]
        public async Task<IActionResult> GerMakeById([FromQuery] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetAllMakesRequest());

            return NewResult(result);
        }
        
        [HttpGet("make-name")]
        public async Task<IActionResult> GerMakeById([FromQuery] string makeName)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetMakeByNameRequest(makeName));

            return NewResult(result);
        }
        #endregion
        
        
        #region POST
        [HttpPost()]
        [Route("create-make")]
        public async Task<IActionResult> CreateMake([FromBody] CreateMakeDto makeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new CreateMakeRequest(makeDto));

            return NewResult(result);
        }
        
        [HttpPost()]
        [Route("create-makes-list")]
        public async Task<IActionResult> CreateMakesRenge([FromBody] IEnumerable<CreateMakeDto> makesDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new CreateMakesRangeRequest(makesDto));

            return NewResult(result);
        }
        #endregion
        
        
        #region PUT
        
        #endregion
        
        
        #region DELETE
        
        #endregion
    }
}
