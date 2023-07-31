using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Area : BaseEntity
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        [ForeignKey(nameof(DefaultPricingPlan))]
        public Guid? DefaultPricingPlanId { get; set; }
        [ForeignKey(nameof(AreaType))]
        public Guid AreaTypeId { get; set; }
        [ForeignKey(nameof(Branch))]
        public Guid BranchId { get; set; }

        public virtual PricingPlan DefaultPricingPlan { get; set; }
        public virtual AreaType AreaType { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual List<PricingPlan> PricingPlans { get; set; }
    }
}
