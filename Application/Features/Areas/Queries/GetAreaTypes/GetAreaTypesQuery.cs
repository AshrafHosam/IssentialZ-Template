using Application.Response;
using MediatR;

namespace Application.Features.Areas.Queries.GetAreaTypes
{
    public class GetAreaTypesQuery : IRequest<ApiResponse<List<GetAreaTypesQueryResponse>>>
    {
    }
}
