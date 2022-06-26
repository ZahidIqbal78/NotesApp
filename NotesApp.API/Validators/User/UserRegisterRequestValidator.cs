using FluentValidation;
using NotesApp.API.DTOs.User;

namespace NotesApp.API.Validators.User
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequestDto>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(x=>x.Email).NotEmpty().NotNull().EmailAddress().MaximumLength(150);
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
