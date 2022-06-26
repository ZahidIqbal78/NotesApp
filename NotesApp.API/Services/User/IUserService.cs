using NotesApp.API.DomainModels;
using NotesApp.API.DTOs.User;

namespace NotesApp.API.Services.User
{
    public interface IUserService
    {
        void Register(UserRegisterRequestDto newUser);
        IEnumerable<ApplicationUser> GetAll();
        bool CheckExistingUser(string email);
        ApplicationUser? GetUser(string email);
        ApplicationUser? GetUserById(int id);
        UserLoginResponseDto Login(ApplicationUser user);
    }
}
