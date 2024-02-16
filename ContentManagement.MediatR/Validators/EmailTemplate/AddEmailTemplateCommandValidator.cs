using FluentValidation;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.MediatR.Validators
{
    public class AddEmailTemplateCommandValidator : AbstractValidator<AddEmailTemplateCommand>
    {
        public AddEmailTemplateCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Subject).NotEmpty().WithMessage("Subject is required");
            RuleFor(c => c.Body).NotEmpty().WithMessage("Body is required");
        }
    }
}
