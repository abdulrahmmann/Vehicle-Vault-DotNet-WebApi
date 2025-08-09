using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.DriveTypesFeature.Commands.Requests;
using VehicleVault.Application.Features.DriveTypesFeature.DTOs;
using VehicleVault.Application.Features.DriveTypesFeature.Queries.Requests;

namespace VehicleVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class DriveTypeController: AppControllerBase
    {
        #region GET
        [HttpGet]
        [Route("drive-type-list")]
        public async Task<IActionResult> GetDriveTypesList()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetAllDriveTypesRequest());

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("by-id")]
        public async Task<IActionResult> GetDriveTypeById([FromQuery] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetDriveTypeByIdRequest(id));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("by-name")]
        public async Task<IActionResult> GetDriveTypeByName([FromQuery] string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new GetDriveTypeByNameRequest(name));

            return NewResult(result);
        }
        #endregion 
        
        
        #region POST
        [HttpPost]
        [Route("create-drive-type")]
        public async Task<IActionResult> CreateDriveType([FromBody] CreateDriveTypeDto driveTypeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new CreateDriveTypeRequest(driveTypeDto));

            return NewResult(result);
        }
        
        [HttpPost]
        [Route("create-drive-type-list")]
        public async Task<IActionResult> CreateDriveType([FromBody] IEnumerable<CreateDriveTypeDto> driveTypesDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new CreateDriveTypeListRequest(driveTypesDto));

            return NewResult(result);
        }
        #endregion 
        
        
        #region DELETE
        [HttpDelete]
        [Route("delete-drive-type")]
        public async Task<IActionResult> DeleteDriveType([FromQuery] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new SoftDeleteDriveTypeRequest(id));

            return NewResult(result);
        }
        #endregion 
        
        
        #region PUT & PATCH
        [HttpPatch]
        [Route("restore-drive-type")]
        public async Task<IActionResult> RestoreDriveType([FromQuery] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(new RestoreDriveTypeRequest(id));

            return NewResult(result);
        }
        #endregion 
    }
}
