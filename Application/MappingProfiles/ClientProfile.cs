using Application.Features.Clients.Commands.AddClient;
using Application.Features.Clients.Queries.GetClient;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, GetClientQueryResponse>();
            CreateMap<AddClientCommand, Client>();
        }
    }
}
