using API.Attributes;
using Application.Features.BrandCosts.Commands.AddEditBrandCostCategory;
using Application.Features.BrandCosts.Queries.GetBrandCostCategories;
using Application.Features.BrandCustomServices.Commands.AddEditCustomServiceCategory;
using Application.Response;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandCostController : BaseController
    {
        private readonly IMediator _mediator;

        public BrandCostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("AddEditBrandCostCategory")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(AddEditBrandCostCategoryCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<AddEditBrandCostCategoryCommandResponse>> AddEditBrandCostCategory(AddEditBrandCostCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetBrandCostCategories")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(List<GetBrandCostCategoriesQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetBrandCostCategoriesQueryResponse>>> GetBrandCostCategories([FromQuery] GetBrandCostCategoriesQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }
    }
}
