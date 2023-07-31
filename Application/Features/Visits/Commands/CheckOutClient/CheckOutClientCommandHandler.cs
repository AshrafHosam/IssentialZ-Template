using Application.Contracts.Repos;
using Application.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.Visits.Commands.CheckOutClient
{
    internal class CheckOutClientCommandHandler : IRequestHandler<CheckOutClientCommand, ApiResponse<CheckOutClientCommandResponse>>
    {
        private readonly ISharedAreaVisitRepo _sharedAreaVisitRepo;
        private readonly IAreaRepo _areaRepo;
        public CheckOutClientCommandHandler(ISharedAreaVisitRepo sharedAreaVisitRepo, IAreaRepo areaRepo)
        {
            _sharedAreaVisitRepo = sharedAreaVisitRepo;
            _areaRepo = areaRepo;
        }

        public async Task<ApiResponse<CheckOutClientCommandResponse>> Handle(CheckOutClientCommand request, CancellationToken cancellationToken)
        {
            var isVisitExist = await _sharedAreaVisitRepo.AnyAsync(request.VisitId);
            if (!isVisitExist)
                return ApiResponse<CheckOutClientCommandResponse>.GetNotFoundApiResponse();

            var visit = await _sharedAreaVisitRepo.GetAsync(request.VisitId);
            if (visit.CheckOutStamp.HasValue)
                return ApiResponse<CheckOutClientCommandResponse>.GetBadRequestApiResponse(new List<string> { "This Visit is Checked Out Already" });

            CalculateServies(request, visit);

            var area = await _areaRepo.GetAreaPricingPlansIncluded(visit.AreaId);

            CalculateTotal(visit, area.DefaultPricingPlan.MaxUnitsNumber, area.DefaultPricingPlan.PricePerUnit);

            if (request.IsSubmitted)
                visit.CheckOutStamp = DateTimeOffset.UtcNow;

            await _sharedAreaVisitRepo.UpdateAsync(visit);

            return ApiResponse<CheckOutClientCommandResponse>
                .GetSuccessApiResponse(new CheckOutClientCommandResponse
                {
                    Total = visit.TotalAmount
                });
        }

        private void CalculateTotal(SharedAreaVisit visit, int maxUnitsNumber, decimal pricePerHour)
        {
            TimeSpan timeSpan = DateTimeOffset.UtcNow.Subtract(visit.CheckInStamp);
            double hours = timeSpan.TotalHours;

            if (hours >= (double)maxUnitsNumber)
                hours = (double)maxUnitsNumber;

            visit.TotalAmount = (pricePerHour * (decimal)hours) + visit.CustomServices.Select(a => a.ServicePrice).Sum();
        }

        private void CalculateServies(CheckOutClientCommand request, SharedAreaVisit visit)
        {
            foreach (var service in request.Services)
            {
                if (service.Id.HasValue)
                {
                    var visitService = visit.CustomServices.FirstOrDefault(a => a.Id == service.Id);
                    if (visitService != null)
                        visitService.ServicePrice = service.ServicePrice;
                    else
                        visit.CustomServices.Add(new Domain.Entities.CustomService
                        {
                            ServicePrice = service.ServicePrice,
                            ServiceName = service.ServiceName
                        });
                }
                else
                    visit.CustomServices.Add(new Domain.Entities.CustomService
                    {
                        ServicePrice = service.ServicePrice,
                        ServiceName = service.ServiceName
                    });
            }
        }
    }
}
