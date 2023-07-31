using Application.Response;
using Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Areas.Commands.CreateArea
{
    public class CreateAreaCommand : IRequest<ApiResponse<CreateAreaCommandResponse>>
    {
        [Required]
        public string Name { get; set; }
        public int Capacity { get; set; }
        [Required]
        public Guid AreaTypeId { get; set; }
        [Required]
        public Guid BranchId { get; set; }
        public Guid? DefaultPricingPlanId { get; set; }
        public PricingPlanTimeSpanEnum? PricingUnit { get; set; }
        public int? MaxUnitsNumber { get; set; }
        public decimal? PricePerUnit { get; set; }
        public List<Guid> PricingPlanIds { get; set; }
    }
}
