namespace NotesApp.API.DTOs.Note
{
    public class AddNoteRequestDto
    {
        public string NoteType { get; set; }

        public string NoteText { get; set; }

        public DateTime? ReminderOrDueDate { get; set; }
        public bool? IsComplete { get; set; }
    }
}
