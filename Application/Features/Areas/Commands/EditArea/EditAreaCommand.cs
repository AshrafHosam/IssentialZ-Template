using Application.Response;
using Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Areas.Commands.EditArea
{
    public class EditAreaCommand : IRequest<ApiResponse<EditAreaCommandResponse>>
    {
        [Required]
        public Guid AreaId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid AreaTypeId { get; set; }
        [Required]
        public Guid BranchId { get; set; }
        public int Capacity { get; set; }
        public Guid DefaultPricingPlanId { get; set; }
        public PricingPlanTimeSpanEnum PricingUnit { get; set; }
        public int MaxUnitsNumber { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
