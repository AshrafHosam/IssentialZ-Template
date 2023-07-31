using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Areas.Commands.EditArea
{
    public class EditAreaCommandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AreaTypeId { get; set; }
        public Guid BranchId { get; set; }
        public int Capacity { get; set; }
        public Guid DefaultPricingPlanId { get; set; }
        public PricingPlanTimeSpanEnum PricingUnit { get; set; }
        public int MaxUnitsNumber { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}