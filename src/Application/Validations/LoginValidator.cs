using FluentValidation;
using Application.ViewModels.Login;

namespace Application.ViewModels.Validations
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(o => o.Email)
                .EmailAddress().WithMessage("{PropertyName} invalid")
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(o => o.Password)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
