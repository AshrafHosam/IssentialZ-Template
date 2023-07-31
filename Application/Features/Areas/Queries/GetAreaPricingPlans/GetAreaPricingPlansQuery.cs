using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Areas.Queries.GetAreaPricingPlans
{
    public class GetAreaPricingPlansQuery : IRequest<ApiResponse<List<GetAreaPricingPlansQueryResponse>>>
    {
        [Required]
        public Guid AreaId { get; set; }
    }
}
