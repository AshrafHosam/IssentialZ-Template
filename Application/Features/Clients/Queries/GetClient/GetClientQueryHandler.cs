using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Clients.Queries.GetClient
{
    public class GetClientQueryHandler : IRequestHandler<GetClientQuery, ApiResponse<List<GetClientQueryResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepo _clientRepo;

        public GetClientQueryHandler(IMapper mapper, IClientRepo clientRepo)
        {
            _mapper = mapper;
            _clientRepo = clientRepo;
        }

        public async Task<ApiResponse<List<GetClientQueryResponse>>> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            if (request.Id.HasValue)
            {
                var clientEntity = await _clientRepo.GetAsync(request.Id.Value);
                if (clientEntity == null)
                    return ApiResponse<List<GetClientQueryResponse>>.GetNotFoundApiResponse();

                return new ApiResponse<List<GetClientQueryResponse>>
                {
                    Data = new List<GetClientQueryResponse> { _mapper.Map<GetClientQueryResponse>(clientEntity) }
                };
            }

            var clients = await _clientRepo.SearchForClient(request.SearchText);

            return ApiResponse<List<GetClientQueryResponse>>.GetSuccessApiResponse(_mapper.Map<List<GetClientQueryResponse>>(clients));
        }
    }
}
