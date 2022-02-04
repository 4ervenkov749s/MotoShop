using MotoShop.Models.Requests;
using FluentValidation;

namespace MotoShop.Validators
{
    public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
    {
        public EmployeeRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(2).MaximumLength(25);
            RuleFor(x => x.Competence).NotNull();
            RuleFor(x => x.Salary).NotNull().NotEmpty().GreaterThanOrEqualTo(650);
        }
    }
}
