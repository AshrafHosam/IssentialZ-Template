using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IAreaRepo : IBaseRepo<Area>
    {
        Task<List<AreaType>> GetAreaTypesAsync();
        Task<Area> GetAreaPricingPlansIncluded(Guid AreaId);
        Task<List<Area>> GetAreasByBranch(Guid branchId);
    }
}
