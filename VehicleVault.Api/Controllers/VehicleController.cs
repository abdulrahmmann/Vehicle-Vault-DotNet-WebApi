using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.VehiclesFeature.Queries.Requests;

namespace VehicleVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class VehicleController : AppControllerBase
    {
        [HttpGet]
        [Route("vehicles-list")]
        public async Task<IActionResult> GetAllVehicles()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetAllVehiclesRequest());

            return NewResult(result);
        }
    }
}
