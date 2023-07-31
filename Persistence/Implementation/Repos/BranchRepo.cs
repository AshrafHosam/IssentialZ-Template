using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class BranchRepo : BaseRepo<Branch>, IBranchRepo
    {
        public BranchRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Branch>> GetBranchesByBrandId(Guid brandId)
        {
            return await _context.Branches
                .Where(a => a.BrandId == brandId)
                .ToListAsync();
        }
    }
}
