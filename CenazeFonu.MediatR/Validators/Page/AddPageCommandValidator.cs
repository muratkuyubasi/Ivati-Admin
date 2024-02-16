using CenazeFonu.MediatR.Commands;
using FluentValidation;

namespace CenazeFonu.MediatR.Validators
{
    public class AddPageCommandValidator:  AbstractValidator<AddPageCommand>
    {
        public AddPageCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
