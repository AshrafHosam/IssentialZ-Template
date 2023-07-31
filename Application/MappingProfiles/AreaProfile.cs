using Application.Features.Areas.Commands.CreateArea;
using Application.Features.Areas.Commands.EditArea;
using Application.Features.Areas.Queries.GetArea;
using Application.Features.Areas.Queries.GetAreaTypes;
using Application.Features.Branches.Queries.GetBranchAreas;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class AreaProfile : Profile
    {
        public AreaProfile()
        {
            CreateMap<CreateAreaCommand, Area>();

            CreateMap<AreaType, GetAreaTypesQueryResponse>();

            CreateMap<EditAreaCommand, Area>()
                .ForPath(dest => dest.DefaultPricingPlan.MaxUnitsNumber, opt => opt.MapFrom(src => src.MaxUnitsNumber))
                .ForPath(dest => dest.DefaultPricingPlan.PricePerUnit, opt => opt.MapFrom(src => src.PricePerUnit))
                .ForPath(dest => dest.DefaultPricingPlan.PricingUnit, opt => opt.MapFrom(src => src.PricingUnit))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AreaId));

            CreateMap<EditAreaCommandResponse, Area>()
                .ForPath(dest => dest.DefaultPricingPlan.MaxUnitsNumber, opt => opt.MapFrom(src => src.MaxUnitsNumber))
                .ForPath(dest => dest.DefaultPricingPlan.PricePerUnit, opt => opt.MapFrom(src => src.PricePerUnit))
                .ForPath(dest => dest.DefaultPricingPlan.PricingUnit, opt => opt.MapFrom(src => src.PricingUnit))
                .ReverseMap();

            CreateMap<GetAreaQueryResponse, Area>()
                .ForPath(dest => dest.DefaultPricingPlan.MaxUnitsNumber, opt => opt.MapFrom(src => src.MaxUnitsNumber))
                .ForPath(dest => dest.DefaultPricingPlan.PricePerUnit, opt => opt.MapFrom(src => src.PricePerUnit))
                .ForPath(dest => dest.DefaultPricingPlan.PricingUnit, opt => opt.MapFrom(src => src.PricingUnit))
                .ReverseMap();

            CreateMap<Area, GetBranchAreasQueryResponse>()
                .ForMember(dest => dest.PricingUnit, opt => opt.MapFrom(src => src.DefaultPricingPlan.PricingUnit))
                .ForMember(dest => dest.PricePerUnit, opt => opt.MapFrom(src => src.DefaultPricingPlan.PricePerUnit))
                .ForMember(dest => dest.MaxUnitsNumber, opt => opt.MapFrom(src => src.DefaultPricingPlan.MaxUnitsNumber))
                .ForMember(dest => dest.AreaTypeName, opt => opt.MapFrom(src => src.AreaType.Name));
        }
    }
}
