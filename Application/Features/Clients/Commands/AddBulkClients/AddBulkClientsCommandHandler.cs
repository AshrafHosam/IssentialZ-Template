using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Clients.Commands.AddBulkClients
{
    public class AddBulkClientsCommandHandler : IRequestHandler<AddBulkClientsCommand, ApiResponse<AddBulkClientsCommandResponse>>
    {
        private readonly IBrandRepo _brandRepo;
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;

        public AddBulkClientsCommandHandler(IBrandRepo brandRepo, IClientRepo clientRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _clientRepo = clientRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<AddBulkClientsCommandResponse>> Handle(AddBulkClientsCommand request, CancellationToken cancellationToken)
        {
            var brandId = request.Clients.Select(a => a.BrandId).Distinct().ToList();
            if (brandId.Count != 1)
                return ApiResponse<AddBulkClientsCommandResponse>.GetBadRequestApiResponse(new List<string> { "Can't have more than one brand" });

            var brandExist = await _brandRepo.AnyAsync(brandId.First());
            if (!brandExist)
                return ApiResponse<AddBulkClientsCommandResponse>.GetNotFoundApiResponse();

            var addedClients = await _clientRepo.AddRangeAsync(_mapper.Map<List<Domain.Entities.Client>>(request.Clients));
            if (addedClients.Count() == request.Clients.Count)
                return ApiResponse<AddBulkClientsCommandResponse>.GetNoContentApiResponse();

            return ApiResponse<AddBulkClientsCommandResponse>.GetBadRequestApiResponse();
        }
    }
}
