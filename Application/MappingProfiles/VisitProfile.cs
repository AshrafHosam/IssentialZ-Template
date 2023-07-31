using AutoMapper;
using Domain.Entities;
using Issentialz.Application.Features.Visits.Queries.GetCheckedInClients;

namespace Application.MappingProfiles
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<SharedAreaVisit, GetCheckedInClientsQueryResponse>();
        }
    }
}
