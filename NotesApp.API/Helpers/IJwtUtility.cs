using NotesApp.API.DomainModels;

namespace NotesApp.API.Helpers
{
    public interface IJwtUtility
    {
        string GenerateToken(ApplicationUser user);
        int? ValidateToken(string token);
    }
}
