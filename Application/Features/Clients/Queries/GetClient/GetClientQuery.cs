using Application.Response;
using MediatR;

namespace Application.Features.Clients.Queries.GetClient
{
    public class GetClientQuery : IRequest<ApiResponse<List<GetClientQueryResponse>>>
    {
        public Guid? Id { get; set; }
        public string SearchText { get; set; }
    }
}
