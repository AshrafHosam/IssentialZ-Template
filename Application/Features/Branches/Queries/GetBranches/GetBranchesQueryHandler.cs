using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Branches.Queries.GetBranches
{
    public class GetBranchesQueryHandler : IRequestHandler<GetBranchesQuery, ApiResponse<List<GetBranchesQueryResponse>>>
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IMapper _mapper;
        private readonly IBrandRepo _brandRepo;
        public GetBranchesQueryHandler(IBranchRepo branchRepo, IMapper mapper, IBrandRepo brandRepo)
        {
            _branchRepo = branchRepo;
            _mapper = mapper;
            _brandRepo = brandRepo;
        }

        public async Task<ApiResponse<List<GetBranchesQueryResponse>>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepo.GetAsync(request.BrandId);
            if (brand == null)
                return ApiResponse<List<GetBranchesQueryResponse>>.GetNotFoundApiResponse();

            var branches = await _branchRepo.GetBranchesByBrandId(request.BrandId);

            return ApiResponse<List<GetBranchesQueryResponse>>.GetSuccessApiResponse(_mapper.Map<List<GetBranchesQueryResponse>>(branches));
        }
    }
}
