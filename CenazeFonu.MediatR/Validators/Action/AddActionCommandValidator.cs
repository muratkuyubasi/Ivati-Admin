using CenazeFonu.MediatR.Commands;
using FluentValidation;

namespace CenazeFonu.MediatR.Validators
{
    public class AddActionCommandValidator: AbstractValidator<AddActionCommand>
    {
        public AddActionCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
