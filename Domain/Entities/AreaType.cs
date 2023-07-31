using Domain.Common;

namespace Domain.Entities
{
    public class AreaType : BaseEntity
    {
        public string Name { get; set; }
        
        public virtual IEnumerable<PricingPlan> PricingPlans { get; set; }
    }
}
