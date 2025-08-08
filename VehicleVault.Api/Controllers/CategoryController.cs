using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleVault.Application.Features.CategoryFeature.Commands.Requests;
using VehicleVault.Application.Features.CategoryFeature.DTOs;

namespace VehicleVault.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryController : AppControllerBase
    {
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
    }
}
