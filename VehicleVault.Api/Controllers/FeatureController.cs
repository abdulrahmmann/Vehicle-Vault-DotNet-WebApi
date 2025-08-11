using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.FeaturesVFeature.Commands.Requests;
using VehicleVault.Application.Features.FeaturesVFeature.DTOs;
using VehicleVault.Application.Features.FeaturesVFeature.Queries.Requests;

namespace VehicleVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class FeatureController : AppControllerBase
    {
        [HttpPost]
        [Route("create-feature")]
        public async Task<IActionResult> CreateFeature([FromBody] CreateFeatureDto featureDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new CreateFeatureRequest(featureDto));

            return NewResult(result);
        }
        
        [HttpPost]
        [Route("create-features-list")]
        public async Task<IActionResult> CreateFeatureList([FromBody] IEnumerable<CreateFeatureDto> featuresDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new CreateFeaturesListRequest(featuresDto));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("features-list")]
        public async Task<IActionResult> GetAllFeaturesList()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetAllFeaturesRequest());

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("feature-id")]
        public async Task<IActionResult> GetFeatureById([FromQuery] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetFeatureByIdRequest(id));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("feature-name")]
        public async Task<IActionResult> GetFeatureByName([FromQuery] string name)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetFeatureByNameRequest(name));

            return NewResult(result);
        }
    }
}
