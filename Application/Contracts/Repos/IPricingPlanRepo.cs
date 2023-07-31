using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IPricingPlanRepo : IBaseRepo<PricingPlan>
    {
        Task<List<PricingPlan>> GetPlansByIds(List<Guid> pricingPlanIds);

        Task DeletePricingPlans(List<PricingPlan> pricingPlans);
    }
}
