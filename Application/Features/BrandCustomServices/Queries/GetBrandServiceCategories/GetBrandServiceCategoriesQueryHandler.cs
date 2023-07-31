using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.BrandCustomServices.Queries.GetBrandServiceCategories
{
    public class GetBrandServiceCategoriesQueryHandler : IRequestHandler<GetBrandServiceCategoriesQuery, ApiResponse<List<GetBrandServiceCategoriesQueryResponse>>>
    {
        private readonly IBrandRepo _brandRepo;
        private readonly ICustomServiceCategoryRepo _customServiceCategoryRepo;
        private readonly IMapper _mapper;

        public GetBrandServiceCategoriesQueryHandler(IBrandRepo brandRepo, ICustomServiceCategoryRepo customServiceCategoryRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _customServiceCategoryRepo = customServiceCategoryRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetBrandServiceCategoriesQueryResponse>>> Handle(GetBrandServiceCategoriesQuery request, CancellationToken cancellationToken)
        {
            var brandExist = await _brandRepo.AnyAsync(request.BrandId);
            if (!brandExist)
                return ApiResponse<List<GetBrandServiceCategoriesQueryResponse>>.GetNotFoundApiResponse();

            var categories = await _customServiceCategoryRepo.GetBrandServiceCategories(request.BrandId);

            return ApiResponse<List<GetBrandServiceCategoriesQueryResponse>>.GetSuccessApiResponse(_mapper.Map<List<GetBrandServiceCategoriesQueryResponse>>(categories));
        }
    }
}
