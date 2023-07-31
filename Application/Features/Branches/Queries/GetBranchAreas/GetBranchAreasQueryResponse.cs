using Application.Features.Areas.Queries.GetArea;
using Domain.Enums;

namespace Application.Features.Branches.Queries.GetBranchAreas
{
    public class GetBranchAreasQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AreaTypeId { get; set; }
        public string AreaTypeName { get; set; }
        public Guid BranchId { get; set; }
        public int Capacity { get; set; }
        public Guid DefaultPricingPlanId { get; set; }
        public PricingPlanTimeSpanEnum PricingUnit { get; set; }
        public int MaxUnitsNumber { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}