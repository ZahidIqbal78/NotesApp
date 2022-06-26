using System.Text.Json.Serialization;

namespace NotesApp.API.DTOs.User
{
    public class UserRegisterRequestDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
