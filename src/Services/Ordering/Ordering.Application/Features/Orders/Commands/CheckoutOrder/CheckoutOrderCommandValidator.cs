
using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(a => a.UserName)
                .NotEmpty().WithMessage("{UserName is required.}")
                .NotNull().WithMessage("{UserName is required.}")
                .MaximumLength(50).WithMessage("{UserName must not exceed 50 character.}");

            RuleFor(a => a.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress is required.}")
                .NotNull().WithMessage("{EmailAddress is required.}")
                .EmailAddress();

            RuleFor(a => a.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice is required.}")
                .GreaterThan(0).WithMessage("{TotalPrice should be greater than 0.}");
        }
    }
}
