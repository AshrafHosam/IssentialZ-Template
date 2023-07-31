using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class ClientRepo : BaseRepo<Client>, IClientRepo
    {
        public ClientRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Client>> GetBrandClientsPaginated(Guid brandId)
        {
            return await _context.Clients
                .Where(a => a.BrandId == brandId)
                .OrderByDescending(a => a.CreatedDate)
                .Skip(0)
                .Take(50)
                .ToListAsync();
        }

        public async Task<List<Client>> SearchForClient(string searchText)
        {
            return await _context.Clients
                .Where(a => a.Name.Contains(searchText) ||
                a.Email.Contains(searchText) ||
                a.MobileNumber.Contains(searchText))
                .Skip(0)
                .Take(10)
                .ToListAsync();
        }

    }
}
