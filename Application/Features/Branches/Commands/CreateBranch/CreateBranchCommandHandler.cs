using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Branches.Commands.CreateBranch
{
    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, ApiResponse<CreateBranchCommandResponse>>
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IMapper _mapper;
        private readonly IBrandRepo _brandRepo;
        public CreateBranchCommandHandler(IBranchRepo branchRepo, IMapper mapper, IBrandRepo brandRepo)
        {
            _branchRepo = branchRepo;
            _mapper = mapper;
            _brandRepo = brandRepo;
        }

        public async Task<ApiResponse<CreateBranchCommandResponse>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepo.GetAsync(request.BrandId);

            if (brand == null)
                return new ApiResponse<CreateBranchCommandResponse>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };

            var createdBranch = await _branchRepo.AddAsync(_mapper.Map<Branch>(request));

            if (createdBranch == null)
                return ApiResponse<CreateBranchCommandResponse>.GetBadRequestApiResponse();

            return ApiResponse<CreateBranchCommandResponse>
                .GetSuccessApiResponse(new CreateBranchCommandResponse
                {
                    Id = createdBranch.Id
                });
        }
    }
}
