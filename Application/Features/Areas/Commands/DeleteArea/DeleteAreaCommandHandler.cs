using Application.Contracts.Repos;
using Application.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.Areas.Commands.DeleteArea
{
    public class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommand, ApiResponse<DeleteAreaCommandResponse>>
    {
        private readonly IAreaRepo _areaRepo;
        private readonly IPricingPlanRepo _pricingPlanRepo;
        public DeleteAreaCommandHandler(IAreaRepo areaRepo, IPricingPlanRepo pricingPlanRepo)
        {
            _areaRepo = areaRepo;
            _pricingPlanRepo = pricingPlanRepo;
        }

        public async Task<ApiResponse<DeleteAreaCommandResponse>> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
        {
            var area = await _areaRepo.GetAreaPricingPlansIncluded(request.AreaId);
            if (area == null)
                return ApiResponse<DeleteAreaCommandResponse>.GetNotFoundApiResponse();

            var pricingPlans = new List<PricingPlan>();

            if (area.PricingPlans != null && area.PricingPlans.Any())
                area.PricingPlans.ForEach(a => pricingPlans.Add(a));

            if (area.DefaultPricingPlan != null)
                pricingPlans.Add(area.DefaultPricingPlan);

            await _pricingPlanRepo.DeletePricingPlans(pricingPlans);

            await _areaRepo.DeleteAsync(area);

            return ApiResponse<DeleteAreaCommandResponse>.GetNoContentApiResponse();
        }
    }
}
