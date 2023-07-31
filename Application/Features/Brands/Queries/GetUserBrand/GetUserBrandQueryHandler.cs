using Application.Contracts.Identity;
using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Brands.Queries.GetUserBrand
{
    public class GetUserBrandQueryHandler : IRequestHandler<GetUserBrandQuery, ApiResponse<GetUserBrandQueryResponse>>
    {
        private readonly IClaimService _claimService;
        private readonly IBrandRepo _brandRepo;
        private readonly IMapper _mapper;
        public GetUserBrandQueryHandler(IClaimService claimService, IBrandRepo brandRepo, IMapper mapper)
        {
            _claimService = claimService;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetUserBrandQueryResponse>> Handle(GetUserBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepo.GetBrandByOwnerId(Guid.Parse(_claimService.GetUserId()));

            if (brand == null)
                return ApiResponse<GetUserBrandQueryResponse>.GetNotFoundApiResponse();

            return ApiResponse<GetUserBrandQueryResponse>.GetSuccessApiResponse(_mapper.Map<GetUserBrandQueryResponse>(brand));
        }
    }
}
