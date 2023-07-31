using API.Attributes;
using Application.Features.Clients.Commands.AddBulkClients;
using Application.Features.Clients.Commands.AddClient;
using Application.Features.Clients.Queries.GetBrandClients;
using Application.Features.Clients.Queries.GetClient;
using Application.Response;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : BaseController
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("AddClient")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(AddClientCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<AddClientCommandResponse>> AddClient([FromBody] AddClientCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("AddBulkClients")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddBulkClients([FromBody] AddBulkClientsCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetClient")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(List<GetClientQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetClient([FromQuery] GetClientQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetBrandClients")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(List<GetClientQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBrandClients([FromQuery] GetBrandClientsQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }
    }
}
