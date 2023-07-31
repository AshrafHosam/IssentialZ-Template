using API.Attributes;
using Application.Features.Visits.Commands.CheckOutClient;
using Application.Response;
using Identity.Enums;
using Issentialz.Application.Features.Visits.Commands.CheckInClient;
using Issentialz.Application.Features.Visits.Queries.GetCheckedInClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VisitsController : BaseController
    {
        private readonly IMediator _mediator;

        public VisitsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetCheckedInClients")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<GetCheckedInClientsQueryResponse>>> GetCheckedInClients([FromQuery] GetCheckedInClientsQuery query)
        {
            var result = await _mediator.Send(query);
            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CheckInClient")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckInClientCommandResponse>> CheckInClient(CheckInClientCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CheckOutClient")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(CheckOutClientCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<CheckOutClientCommandResponse>> CheckOutClient(CheckOutClientCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
    }
}
