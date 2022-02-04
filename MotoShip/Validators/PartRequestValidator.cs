using MotoShop.Models.Requests;
using FluentValidation;

namespace MotoShop.Validators
{
    public class PartRequestValidator : AbstractValidator<PartRequest>
    {
        public PartRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(2).MaximumLength(20);
            RuleFor(x => x.PartNumber).NotNull();
            RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
