using FluentValidation;
using TaskManager.Domain.Models.Request;

namespace TaskManager.Infra.Validations
{
    public class AuthValidation : AbstractValidator<AuthRequest>
    {
        private string _parameterRequired = "Parameter '{0}' must be sent.";

        public AuthValidation()
        {
            RuleFor(x => x.User)
               .NotEmpty()
               .WithMessage(string.Format(_parameterRequired, "User"));

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage(string.Format(_parameterRequired, "Password"));
        }
    }
}
