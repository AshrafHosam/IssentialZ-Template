using Application.Response;
using MediatR;

namespace Application.Features.Brands.Queries.GetUserBrand
{
    public class GetUserBrandQuery : IRequest<ApiResponse<GetUserBrandQueryResponse>>
    {
    }
}
