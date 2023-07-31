using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class PricingPlanRepo : BaseRepo<PricingPlan>, IPricingPlanRepo
    {
        public PricingPlanRepo(AppDbContext context) : base(context)
        {
        }

        public async Task DeletePricingPlans(List<PricingPlan> pricingPlans)
        {
            _context.PricingPlans.RemoveRange(pricingPlans);

            await _context.SaveChangesAsync();
        }

        public async Task<List<PricingPlan>> GetPlansByIds(List<Guid> pricingPlanIds)
        {
            return await _context.PricingPlans
                .Where(a => pricingPlanIds.Contains(a.Id))
                .ToListAsync();
        }
    }
}
