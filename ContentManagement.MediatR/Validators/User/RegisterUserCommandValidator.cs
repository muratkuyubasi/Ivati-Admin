using ContentManagement.MediatR.Commands;
using FluentValidation;

namespace ContentManagement.MediatR.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Lütfen geçerli bir TC Kimlik no giriniz.");
            RuleFor(c => c.FatherName).NotEmpty().WithMessage("Lütfen baba adınızı giriniz.");
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Lütfen ana adınızı giriniz.");
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("Lütfen adınızı giriniz.");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Lütfen soyadınızı giriniz.");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Lütfen e-posta adresinizi girinizl .");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Lütfen şifrenizi giriniz.");
        }
    }
}
