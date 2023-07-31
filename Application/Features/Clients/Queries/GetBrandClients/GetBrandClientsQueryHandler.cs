using Application.Contracts.Repos;
using Application.Features.Clients.Queries.GetClient;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Clients.Queries.GetBrandClients
{
    internal class GetBrandClientsQueryHandler : IRequestHandler<GetBrandClientsQuery, ApiResponse<List<GetClientQueryResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepo _clientRepo;
        private readonly IBrandRepo _brandRepo;
        public GetBrandClientsQueryHandler(IMapper mapper, IClientRepo clientRepo, IBrandRepo brandRepo)
        {
            _mapper = mapper;
            _clientRepo = clientRepo;
            _brandRepo = brandRepo;
        }

        public async Task<ApiResponse<List<GetClientQueryResponse>>> Handle(GetBrandClientsQuery request, CancellationToken cancellationToken)
        {
            var brandExists = await _brandRepo.AnyAsync(request.BrandId);
            if (!brandExists)
                return ApiResponse<List<GetClientQueryResponse>>.GetNotFoundApiResponse();

            var brandClients = await _clientRepo.GetBrandClientsPaginated(request.BrandId);
            return ApiResponse<List<GetClientQueryResponse>>
                .GetSuccessApiResponse(_mapper.Map<List<GetClientQueryResponse>>(brandClients));
        }
    }
}
