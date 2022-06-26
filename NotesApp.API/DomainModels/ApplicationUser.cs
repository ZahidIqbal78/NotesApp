using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotesApp.API.DomainModels
{
    public class ApplicationUser : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [JsonIgnore]
        public string HashedPassword { get; set; }
        public DateTime DateOfBirth { get; set; }
        
    }
}
