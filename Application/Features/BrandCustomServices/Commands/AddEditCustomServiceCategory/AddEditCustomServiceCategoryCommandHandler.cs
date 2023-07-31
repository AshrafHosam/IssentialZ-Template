using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.BrandCustomServices.Commands.AddEditCustomServiceCategory
{
    public class AddEditCustomServiceCategoryCommandHandler : IRequestHandler<AddEditCustomServiceCategoryCommand, ApiResponse<AddEditCustomServiceCategoryCommandResponse>>
    {
        private readonly ICustomServiceCategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        private readonly IBrandRepo _brandRepo;
        public AddEditCustomServiceCategoryCommandHandler(ICustomServiceCategoryRepo categoryRepo, IMapper mapper, IBrandRepo brandRepo)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _brandRepo = brandRepo;
        }

        public async Task<ApiResponse<AddEditCustomServiceCategoryCommandResponse>> Handle(AddEditCustomServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            var brandExist = await _brandRepo.AnyAsync(request.BrandId);
            if (!brandExist)
                return ApiResponse<AddEditCustomServiceCategoryCommandResponse>.GetNotFoundApiResponse();

            if (request.CategoryId.HasValue)
            {
                var category = await _categoryRepo.GetAsync(request.CategoryId.Value);
                if (category == null)
                    return ApiResponse<AddEditCustomServiceCategoryCommandResponse>.GetNotFoundApiResponse();

                _mapper.Map(request, category);

                await _categoryRepo.UpdateAsync(category);

                return ApiResponse<AddEditCustomServiceCategoryCommandResponse>.GetSuccessApiResponse(_mapper.Map<AddEditCustomServiceCategoryCommandResponse>(category));
            }
            else
            {
                var category = await _categoryRepo.AddAsync(_mapper.Map<CustomServiceCategory>(request));
                if (category == null)
                    return ApiResponse<AddEditCustomServiceCategoryCommandResponse>.GetBadRequestApiResponse();

                return ApiResponse<AddEditCustomServiceCategoryCommandResponse>.GetSuccessApiResponse(_mapper.Map<AddEditCustomServiceCategoryCommandResponse>(category));
            }
        }
    }
}
