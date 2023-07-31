using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Branches.Queries.GetBranchAreas
{
    public class GetBranchAreasQueryHandler : IRequestHandler<GetBranchAreasQuery, ApiResponse<List<GetBranchAreasQueryResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IAreaRepo _areaRepo;
        private readonly IBranchRepo _branchRepo;
        public GetBranchAreasQueryHandler(IMapper mapper, IAreaRepo areaRepo, IBranchRepo branchRepo)
        {
            _mapper = mapper;
            _areaRepo = areaRepo;
            _branchRepo = branchRepo;
        }

        public async Task<ApiResponse<List<GetBranchAreasQueryResponse>>> Handle(GetBranchAreasQuery request, CancellationToken cancellationToken)
        {
            var branchExist = await _branchRepo.AnyAsync(request.BranchId);
            if (!branchExist)
                return ApiResponse<List<GetBranchAreasQueryResponse>>.GetBadRequestApiResponse();

            var areas = await _areaRepo.GetAreasByBranch(request.BranchId);

            return ApiResponse<List<GetBranchAreasQueryResponse>>.GetSuccessApiResponse(_mapper.Map<List<GetBranchAreasQueryResponse>>(areas));
        }
    }
}
