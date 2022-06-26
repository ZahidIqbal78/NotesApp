using FluentValidation;
using NotesApp.API.DTOs.User;

namespace NotesApp.API.Validators.User
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequestDto>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(150);
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
