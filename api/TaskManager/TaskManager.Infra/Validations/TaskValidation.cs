using FluentValidation;
using TaskManager.Domain.Models;


namespace TaskManager.Infra.Validations
{
    public class TaskValidation : AbstractValidator<Tasks>
    {
        private string _parameterRequired = "Parameter '{0}' must be sent.";
        public TaskValidation()
        {
            RuleFor(x => x.UserId)
               .NotEmpty()
               .WithMessage(string.Format(_parameterRequired, "UserId"));

            RuleFor(x => x.Description)
               .NotEmpty()
               .WithMessage(string.Format(_parameterRequired, "Description"));
        }
    }
}
