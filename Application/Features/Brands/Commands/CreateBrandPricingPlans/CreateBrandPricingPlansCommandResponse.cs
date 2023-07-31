namespace Application.Features.Brands.Commands.CreateBrandPricingPlans
{
    public class CreateBrandPricingPlansCommandResponse : AddPricingPlanCommand
    {
        public Guid Id { get; set; }
        public string PricingUnitName
        {
            get
            {
                return PricingUnit.ToString();
            }
        }
    }
}
