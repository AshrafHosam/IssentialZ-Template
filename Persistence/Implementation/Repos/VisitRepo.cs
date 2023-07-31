using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class SharedAreaVisitRepo : BaseRepo<SharedAreaVisit>, ISharedAreaVisitRepo
    {
        public SharedAreaVisitRepo(AppDbContext context) : base(context)
        {
        }
        public async Task<List<SharedAreaVisit>> GetCheckedInClientsByBranch(Guid branchId)
        {
            return await _context.SharedAreaVisits.Include(a => a.Client)
                .Where(a => a.BranchId == branchId
                && a.CheckInStamp.Date == DateTimeOffset.UtcNow.Date
                && !a.CheckOutStamp.HasValue)
                .ToListAsync();
        }
    }
}
