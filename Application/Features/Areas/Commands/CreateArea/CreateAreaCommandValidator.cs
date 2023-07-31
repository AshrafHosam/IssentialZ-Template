using FluentValidation;

namespace Application.Features.Areas.Commands.CreateArea
{
    public class CreateAreaCommandValidator : AbstractValidator<CreateAreaCommand>
    {
        public CreateAreaCommandValidator()
        {
            RuleFor(a => a).Must(ValidPricingPlanAttributes).WithMessage("Invalid Default Pricing Model");
        }
        private bool ValidPricingPlanAttributes(CreateAreaCommand command)
        {
            if (command.DefaultPricingPlanId.HasValue)
                return true;

            if (command.MaxUnitsNumber.HasValue
                && command.PricingUnit.HasValue
                && command.PricePerUnit.HasValue)
                return true;

            return false;
        }
    }
}
