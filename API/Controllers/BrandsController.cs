using API.Attributes;
using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.CreateBrandPricingPlans;
using Application.Features.Brands.Queries.GetUserBrand;
using Application.Response;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandsController : BaseController
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CreateBrand")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateBrandCommandResponse>> CreateBrand(CreateBrandCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Receptionist, UserRolesEnum.BranchManager, UserRolesEnum.Owner)]
        [HttpGet("GetUserBrand")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetUserBrandQueryResponse>> GetUserBrand()
        {
            var result = await _mediator.Send(new GetUserBrandQuery());
            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CreateBrandPricingPlans")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateBrandPricingPlansCommandResponse>> CreateBrandPricingPlans(CreateBrandPricingPlansCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }

    }
}
