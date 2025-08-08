using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.FuelTypesFeature.Commands.Requests;
using VehicleVault.Application.Features.FuelTypesFeature.DTOs;
using VehicleVault.Application.Features.FuelTypesFeature.Queries.Requests;

namespace VehicleVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class FuelTypeController : AppControllerBase
    {
        #region GET
        [HttpGet]
        [Route("fueltypes-list")]
        public async Task<IActionResult> GetAllFuelTypes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetAllFuelTypesRequest());

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("by-id")]
        public async Task<IActionResult> GetFuelTypeById([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetFuelTypeByIdRequest(id));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("by-name")]
        public async Task<IActionResult> GetFuelTypeByName([FromQuery] string fuelTypeName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetFuelTypeByNameRequest(fuelTypeName));

            return NewResult(result);
        }
        #endregion
        
        
        #region POST
        [HttpPost]
        [Route("create-fueltype")]
        public async Task<IActionResult> CreateFuelType([FromBody] CreateFuelTypeDto fuelTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new CreateFuelTypeRequest(fuelTypeDto));

            return NewResult(result);
        }
        
        [HttpPost]
        [Route("create-fueltype-list")]
        public async Task<IActionResult> CreateFuelTypesList([FromBody] IEnumerable<CreateFuelTypeDto> fuelTypesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new CreateFuelTypesListRequest(fuelTypesDto));

            return NewResult(result);
        }
        #endregion
        
        
        #region DELETE
        
        #endregion
        
        
        #region PUT & PATCH
        
        #endregion
    }
}
