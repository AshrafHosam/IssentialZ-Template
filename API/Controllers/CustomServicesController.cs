using API.Attributes;
using Application.Features.BrandCustomServices.Commands.AddEditCustomServiceCategory;
using Application.Features.BrandCustomServices.Queries.GetBrandServiceCategories;
using Application.Response;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomServicesController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("AddEditServiceCategory")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(AddEditCustomServiceCategoryCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<AddEditCustomServiceCategoryCommandResponse>> AddEditServiceCategory(AddEditCustomServiceCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetBrandServiceCategories")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(List<GetBrandServiceCategoriesQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetBrandServiceCategoriesQueryResponse>>> GetBrandServiceCategories([FromQuery] GetBrandServiceCategoriesQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }
    }
}
