using MotoShop.Models.Requests;
using FluentValidation;

namespace MotoShop.Validators
{
    public class MotoRequestValidator : AbstractValidator<MotoRequest>
    {
        public MotoRequestValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().NotNull();
            RuleFor(x => x.Model).NotEmpty().NotNull();
        }

    }
}
