using Domain.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PricingPlan : BaseEntity
    {
        public PricingPlanTimeSpanEnum PricingUnit { get; set; } = PricingPlanTimeSpanEnum.Hours;
        [ForeignKey(nameof(AreaType))]
        public Guid? AreaTypeId { get; set; }
        [ForeignKey(nameof(Brand))]
        public Guid? BrandId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int MaxUnitsNumber { get; set; }
        public string Name { get; set; }
        public virtual AreaType AreaType { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
