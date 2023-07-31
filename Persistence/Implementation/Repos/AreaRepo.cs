using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class AreaRepo : BaseRepo<Area>, IAreaRepo
    {
        public AreaRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<Area> GetAreaPricingPlansIncluded(Guid AreaId)
        {
            return await _context.Areas
                .AsNoTracking()
                .Include(a => a.DefaultPricingPlan)
                .Include(a => a.PricingPlans)
                .FirstOrDefaultAsync(a => a.Id == AreaId);
        }

        public async Task<List<Area>> GetAreasByBranch(Guid branchId)
        {
            return await _context.Areas
                .Include(a => a.AreaType)
                .Include(a => a.DefaultPricingPlan)
                .AsNoTracking()
                .Where(a => a.BranchId == branchId)
                .ToListAsync();
        }

        public async Task<List<AreaType>> GetAreaTypesAsync()
        {
            return await _context.AreaTypes
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
