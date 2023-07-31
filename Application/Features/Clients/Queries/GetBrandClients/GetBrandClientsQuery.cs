using Application.Features.Clients.Queries.GetClient;
using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Clients.Queries.GetBrandClients
{
    public class GetBrandClientsQuery : IRequest<ApiResponse<List<GetClientQueryResponse>>>
    {
        [Required]
        public Guid BrandId { get; set; }
    }
}
