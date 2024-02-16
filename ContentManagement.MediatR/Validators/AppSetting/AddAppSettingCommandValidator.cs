using FluentValidation;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.MediatR.Validators
{
    public class AddAppSettingCommandValidator : AbstractValidator<AddAppSettingCommand>
    {
        public AddAppSettingCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Key).NotEmpty().WithMessage("Key is required");
            RuleFor(c => c.Value).NotEmpty().WithMessage("Value is required");
        }
    }
}
