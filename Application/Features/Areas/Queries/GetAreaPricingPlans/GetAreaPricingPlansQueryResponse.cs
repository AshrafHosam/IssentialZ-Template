using Domain.Entities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Features.Areas.Queries.GetAreaPricingPlans
{
    public class GetAreaPricingPlansQueryResponse
    {
        public PricingPlanTimeSpanEnum PricingUnit { get; set; }
        public string PricingUnitName
        {
            get
            {
                return PricingUnit.ToString();
            }
        }
        public decimal PricePerUnit { get; set; }
        public int MaxUnitsNumber { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
