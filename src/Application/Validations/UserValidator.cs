using FluentValidation;
using Application.ViewModels.Login;

namespace Application.ViewModels.Validations
{
    public class UserValidator : AbstractValidator<UserRequest>
    {
        public UserValidator()
        {
            RuleFor(o => o.Email)
                .EmailAddress().WithMessage("{PropertyName} invalid")
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(o => o.Password)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
