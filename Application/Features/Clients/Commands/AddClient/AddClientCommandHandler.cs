using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Clients.Commands.AddClient
{
    public class AddClientCommandHandler : IRequestHandler<AddClientCommand, ApiResponse<AddClientCommandResponse>>
    {
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;
        private readonly IBrandRepo _brandRepo;
        public AddClientCommandHandler(IClientRepo clientRepo, IMapper mapper, IBrandRepo brandRepo)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
            _brandRepo = brandRepo;
        }

        public async Task<ApiResponse<AddClientCommandResponse>> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            var brandExist = await _brandRepo.AnyAsync(request.BrandId);
            if (!brandExist)
                return ApiResponse<AddClientCommandResponse>.GetNotFoundApiResponse();

            var createdClient = await _clientRepo.AddAsync(_mapper.Map<Domain.Entities.Client>(request));

            return ApiResponse<AddClientCommandResponse>
                .GetSuccessApiResponse(new AddClientCommandResponse
                {
                    Id = createdClient.Id
                });
        }
    }
}
