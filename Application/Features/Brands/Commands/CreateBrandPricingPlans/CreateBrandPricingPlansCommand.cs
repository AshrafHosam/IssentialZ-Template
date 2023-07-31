using Application.Response;
using Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Brands.Commands.CreateBrandPricingPlans
{
    public class CreateBrandPricingPlansCommand : IRequest<ApiResponse<List<CreateBrandPricingPlansCommandResponse>>>
    {
        [Required]
        [MinLength(1)]
        public List<AddPricingPlanCommand> PricingPlans { get; set; }
    }

    public class AddPricingPlanCommand
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        [Required]
        public decimal PricePerUnit { get; set; }
        public int MaxUnitsNumber { get; set; } = -1;
        [Required]
        public PricingPlanTimeSpanEnum PricingUnit { get; set; }
        public Guid? AreaTypeId { get; set; }
    }
}
