using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IClientRepo : IBaseRepo<Client>
    {
        Task<List<Client>> SearchForClient(string searchText);
        Task<List<Client>> GetBrandClientsPaginated(Guid brandId);
    }
}
