using System.ComponentModel.DataAnnotations;

namespace NotesApp.API.DomainModels
{
    public class Note : BaseModel
    {
        [Required]
        public int ApplicationUserId { get; set; }

        [Required]
        [StringLength(50)]
        public string NoteType { get;set; }

        [StringLength(100)]
        public string NoteText { get; set; }

        public DateTime? ReminderOrDueDate { get; set; }
        public bool? IsComplete { get; set; }
    }
}
