using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.CategoryFeature.Commands.Requests;
using VehicleVault.Application.Features.CategoryFeature.DTOs;
using VehicleVault.Application.Features.CategoryFeature.Queries.Requests;

namespace VehicleVault.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryController : AppControllerBase
    {
        #region GET
        [HttpGet]
        [Route("category-list")]
        public async Task<IActionResult> GetAllCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetAllCategoriesRequest());

            return NewResult(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCategoryById([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetCategoryByIdRequest(id));

            return NewResult(result);
        }
        
        [HttpGet("name")]
        public async Task<IActionResult> GetCategoryByName([FromQuery] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetCategoryByNameRequest(name));

            return NewResult(result);
        }
        #endregion
        
        
        #region POST
        [HttpPost]
        [Route("create-category")]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new CreateCategoryRequest(categoryDto));

            return NewResult(result);
        }

        [HttpPost]
        [Route("create-category-list")]
        public async Task<ActionResult> CreateCategoriesRange([FromBody] IEnumerable<CategoryDto> categoriesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await Mediator.Send(new CreateCategoriesRangeRequest(categoriesDto));
            
            return NewResult(result);
        }
        #endregion


        #region DELETE
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> SoftDeleteCategory([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await Mediator.Send(new SoftDeleteCategoryRequest(id));
            
            return NewResult(result);
        }
        #endregion


        #region PUT & Patch
        [HttpPatch("restore/{id:int}")]
        public async Task<IActionResult> RestoreCategory([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than zero.");

            var result = await Mediator.Send(new RestoreCategoryRequest(id));

            return NewResult(result);
        }
        #endregion
    }
}
