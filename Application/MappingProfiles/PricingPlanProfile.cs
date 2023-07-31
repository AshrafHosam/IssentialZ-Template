using Application.Features.Areas.Commands.CreateArea;
using Application.Features.Areas.Queries.GetAreaPricingPlans;
using Application.Features.Brands.Commands.CreateBrandPricingPlans;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class PricingPlanProfile : Profile
    {
        public PricingPlanProfile()
        {
            CreateMap<CreateAreaCommand, PricingPlan>();

            CreateMap<PricingPlan, GetAreaPricingPlansQueryResponse>();

            CreateMap<Area, GetAreaPricingPlansQueryResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DefaultPricingPlan.Name))
                .ForMember(dest => dest.PricingUnit, opt => opt.MapFrom(src => src.DefaultPricingPlan.PricingUnit))
                .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.MaxUnitsNumber, opt => opt.MapFrom(src => src.DefaultPricingPlan.MaxUnitsNumber));

            CreateMap<PricingPlan, GetAreaPricingPlansQueryResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PricingUnit, opt => opt.MapFrom(src => src.PricingUnit))
                .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.MaxUnitsNumber, opt => opt.MapFrom(src => src.MaxUnitsNumber));

            CreateMap<AddPricingPlanCommand, PricingPlan>()
                .ReverseMap();

            CreateMap<PricingPlan, CreateBrandPricingPlansCommandResponse>();
        }
    }
}
