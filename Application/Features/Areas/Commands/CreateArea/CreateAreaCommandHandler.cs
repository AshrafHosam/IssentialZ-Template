using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Areas.Commands.CreateArea
{
    public class CreateAreaCommandHandler : IRequestHandler<CreateAreaCommand, ApiResponse<CreateAreaCommandResponse>>
    {
        private readonly IAreaRepo _areaRepo;
        private readonly IMapper _mapper;
        private readonly IBranchRepo _branchRepo;
        private readonly IPricingPlanRepo _pricingPlanRepo;
        public CreateAreaCommandHandler(IAreaRepo areaRepo, IMapper mapper, IBranchRepo branchRepo, IPricingPlanRepo pricingPlanRepo)
        {
            _areaRepo = areaRepo;
            _mapper = mapper;
            _branchRepo = branchRepo;
            _pricingPlanRepo = pricingPlanRepo;
        }

        public async Task<ApiResponse<CreateAreaCommandResponse>> Handle(CreateAreaCommand request, CancellationToken cancellationToken)
        {
            List<PricingPlan> pricingPlansEntity = new List<PricingPlan>();
            if (request.PricingPlanIds != null && request.PricingPlanIds.Any())
            {
                pricingPlansEntity = await _pricingPlanRepo.GetPlansByIds(request.PricingPlanIds);
            }

            if (request.DefaultPricingPlanId.HasValue)
            {
                var areaToAdd = _mapper.Map<Area>(request);

                areaToAdd.PricingPlans = pricingPlansEntity;

                var addedArea = await _areaRepo.AddAsync(areaToAdd);

                if (addedArea != null)
                    return ApiResponse<CreateAreaCommandResponse>
                        .GetSuccessApiResponse(new CreateAreaCommandResponse
                        {
                            AreaId = addedArea.Id
                        });
            }
            else
            {
                var branch = await _branchRepo.GetAsync(request.BranchId);

                var area = _mapper.Map<Area>(request);

                area.DefaultPricingPlan = new PricingPlan
                {
                    BrandId = branch.BrandId,
                    MaxUnitsNumber = request.MaxUnitsNumber.Value,
                    PricingUnit = request.PricingUnit.Value,
                    PricePerUnit = request.PricePerUnit.Value,
                    Name = $"{request.Name} - Plan"
                };
                area.PricingPlans = pricingPlansEntity;

                var addedArea = await _areaRepo.AddAsync(area);
                if (addedArea != null)
                    return ApiResponse<CreateAreaCommandResponse>
                        .GetSuccessApiResponse(new CreateAreaCommandResponse
                        {
                            AreaId = addedArea.Id
                        });
            }

            return ApiResponse<CreateAreaCommandResponse>.GetBadRequestApiResponse();
        }
    }
}
