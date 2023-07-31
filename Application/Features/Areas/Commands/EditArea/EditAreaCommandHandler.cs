using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Areas.Commands.EditArea
{
    public class EditAreaCommandHandler : IRequestHandler<EditAreaCommand, ApiResponse<EditAreaCommandResponse>>
    {
        private readonly IAreaRepo _areaRepo;
        private readonly IMapper _mapper;

        public EditAreaCommandHandler(IAreaRepo areaRepo, IMapper mapper)
        {
            _areaRepo = areaRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<EditAreaCommandResponse>> Handle(EditAreaCommand request, CancellationToken cancellationToken)
        {
            var area = await _areaRepo.GetAsync(request.AreaId);
            if (area == null)
                return ApiResponse<EditAreaCommandResponse>.GetNotFoundApiResponse();

            _mapper.Map(request, area);

            await _areaRepo.UpdateAsync(area);

            return ApiResponse<EditAreaCommandResponse>.GetSuccessApiResponse(_mapper.Map<EditAreaCommandResponse>(area));
        }
    }
}
