using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Areas.Queries.GetArea
{
    public class GetAreaQueryHandler : IRequestHandler<GetAreaQuery, ApiResponse<GetAreaQueryResponse>>
    {
        private readonly IAreaRepo _areaRepo;
        private readonly IMapper _mapper;

        public GetAreaQueryHandler(IAreaRepo areaRepo, IMapper mapper)
        {
            _areaRepo = areaRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetAreaQueryResponse>> Handle(GetAreaQuery request, CancellationToken cancellationToken)
        {
            var area = await _areaRepo.GetAreaPricingPlansIncluded(request.AreaId);
            if (area == null)
                return ApiResponse<GetAreaQueryResponse>.GetNotFoundApiResponse();

            return ApiResponse<GetAreaQueryResponse>.GetSuccessApiResponse(_mapper.Map<GetAreaQueryResponse>(area));
        }
    }
}
