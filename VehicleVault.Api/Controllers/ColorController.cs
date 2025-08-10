using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.ColorsFeatures.Commands.Requests;
using VehicleVault.Application.Features.ColorsFeatures.DTOs;
using VehicleVault.Application.Features.ColorsFeatures.Queries.Requests;

namespace VehicleVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class ColorController : AppControllerBase
    {
        [HttpPost]
        [Route("create-color")]
        public async Task<IActionResult> CreateColor([FromBody] CreateColorDto colorDto)
        {
            if (!ModelState.IsValid) return Problem();

            var result = await Mediator.Send(new CreateColorRequest(colorDto));

            return NewResult(result);
        }
        
        [HttpPost]
        [Route("create-colors-list")]
        public async Task<IActionResult> CreateColorsList([FromBody] IEnumerable<CreateColorDto> colorsDto)
        {
            if (!ModelState.IsValid) return Problem();

            var result = await Mediator.Send(new CreateColorsListRequest(colorsDto));

            return NewResult(result);
        }
        
        
        [HttpGet]
        [Route("colors-list")]
        public async Task<IActionResult> GetColorsList()
        {
            if (!ModelState.IsValid) return Problem();

            var result = await Mediator.Send(new GetAllColorsRequest());

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("color-id")]
        public async Task<IActionResult> GetColorById([FromQuery] int id)
        {
            if (!ModelState.IsValid) return Problem();

            var result = await Mediator.Send(new GetColorByIdRequest(id));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("color-name")]
        public async Task<IActionResult> GetColorByName([FromQuery] string colorName)
        {
            if (!ModelState.IsValid) return Problem();

            var result = await Mediator.Send(new GetColorByNameRequest(colorName));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("color-code")]
        public async Task<IActionResult> GetColorByCode([FromQuery] string colorCode)
        {
            if (!ModelState.IsValid) return Problem();

            var result = await Mediator.Send(new GetColorByCodeRequest(colorCode));

            return NewResult(result);
        }
    }
}
