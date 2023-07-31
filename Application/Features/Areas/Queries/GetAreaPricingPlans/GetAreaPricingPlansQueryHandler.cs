using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Areas.Queries.GetAreaPricingPlans
{
    public class GetAreaPricingPlansQueryHandler : IRequestHandler<GetAreaPricingPlansQuery, ApiResponse<List<GetAreaPricingPlansQueryResponse>>>
    {
        private readonly IAreaRepo _areaRepo;
        private readonly IMapper _mapper;
        public GetAreaPricingPlansQueryHandler(IAreaRepo areaRepo, IMapper mapper)
        {
            _areaRepo = areaRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetAreaPricingPlansQueryResponse>>> Handle(GetAreaPricingPlansQuery request, CancellationToken cancellationToken)
        {
            var area = await _areaRepo.GetAreaPricingPlansIncluded(request.AreaId);
            if (area == null)
                return ApiResponse<List<GetAreaPricingPlansQueryResponse>>.GetNotFoundApiResponse();

            List<GetAreaPricingPlansQueryResponse> response = new List<GetAreaPricingPlansQueryResponse>();

            if (area.DefaultPricingPlan != null)
                response.Add(_mapper.Map<GetAreaPricingPlansQueryResponse>(area));

            if (area.PricingPlans.Any())
                response.AddRange(_mapper.Map<List<GetAreaPricingPlansQueryResponse>>(area.PricingPlans));

            return ApiResponse<List<GetAreaPricingPlansQueryResponse>>.GetSuccessApiResponse(response);
        }
    }
}
