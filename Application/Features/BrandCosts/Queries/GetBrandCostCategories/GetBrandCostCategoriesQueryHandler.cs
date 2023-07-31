using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.BrandCosts.Queries.GetBrandCostCategories
{
    public class GetBrandCostCategoriesQueryHandler : IRequestHandler<GetBrandCostCategoriesQuery, ApiResponse<List<GetBrandCostCategoriesQueryResponse>>>
    {
        private readonly IBrandCostCategoryRepo _brandCostCategoryRepo;
        private readonly IMapper _mapper;
        private readonly IBrandRepo _brandRepo;
        public GetBrandCostCategoriesQueryHandler(IBrandCostCategoryRepo brandCostCategoryRepo, IMapper mapper, IBrandRepo brandRepo)
        {
            _brandCostCategoryRepo = brandCostCategoryRepo;
            _mapper = mapper;
            _brandRepo = brandRepo;
        }

        public async Task<ApiResponse<List<GetBrandCostCategoriesQueryResponse>>> Handle(GetBrandCostCategoriesQuery request, CancellationToken cancellationToken)
        {
            var brandExist = await _brandRepo.AnyAsync(request.BrandId);
            if (!brandExist)
                return ApiResponse<List<GetBrandCostCategoriesQueryResponse>>.GetNotFoundApiResponse();

            var brandCostCategories = await _brandCostCategoryRepo.GetBrandCostCategories(request.BrandId);

            return ApiResponse<List<GetBrandCostCategoriesQueryResponse>>.GetSuccessApiResponse(_mapper.Map<List<GetBrandCostCategoriesQueryResponse>>(brandCostCategories));
        }
    }
}
