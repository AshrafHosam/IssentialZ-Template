using Application.Contracts.Identity;
using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrandPricingPlans
{
    public class CreateBrandPricingPlansCommandHandler : IRequestHandler<CreateBrandPricingPlansCommand, ApiResponse<List<CreateBrandPricingPlansCommandResponse>>>
    {
        private readonly IPricingPlanRepo _pricingPlanRepo;
        private readonly IMapper _mapper;
        private readonly IBrandRepo _brandRepo;
        private readonly IClaimService _claimService;
        public CreateBrandPricingPlansCommandHandler(IPricingPlanRepo pricingPlanRepo, IMapper mapper, IClaimService claimService, IBrandRepo brandRepo)
        {
            _pricingPlanRepo = pricingPlanRepo;
            _mapper = mapper;
            _claimService = claimService;
            _brandRepo = brandRepo;
        }

        public async Task<ApiResponse<List<CreateBrandPricingPlansCommandResponse>>> Handle(CreateBrandPricingPlansCommand request, CancellationToken cancellationToken)
        {
            var userBrand = await _brandRepo.GetBrandByOwnerId(Guid.Parse(_claimService.GetUserId()));
            if (userBrand == null)
                return ApiResponse<List<CreateBrandPricingPlansCommandResponse>>
                    .GetNotFoundApiResponse(new List<string> { "No Brand Found For This User" });

            request.PricingPlans.ForEach(a => a.BrandId = userBrand.Id);

            var pricingPlans = await _pricingPlanRepo.AddRangeAsync(_mapper.Map<List<PricingPlan>>(request.PricingPlans));

            if (!pricingPlans.Any())
                return ApiResponse<List<CreateBrandPricingPlansCommandResponse>>.GetBadRequestApiResponse();

            return ApiResponse<List<CreateBrandPricingPlansCommandResponse>>.GetSuccessApiResponse(_mapper.Map<List<CreateBrandPricingPlansCommandResponse>>(pricingPlans));
        }
    }
}
