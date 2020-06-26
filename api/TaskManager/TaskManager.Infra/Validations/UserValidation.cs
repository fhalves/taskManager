using FluentValidation;
using TaskManager.Domain.Models;

namespace TaskManager.Infra.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        private string _parameterRequired = "Parameter '{0}' must be sent.";
        public UserValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage(string.Format(_parameterRequired, "Name"));

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage(string.Format(_parameterRequired, "Email"));

            RuleFor(x => x.Login)
               .NotEmpty()
               .WithMessage(string.Format(_parameterRequired, "Login"));

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage(string.Format(_parameterRequired, "Password"));
        }
    }
}
