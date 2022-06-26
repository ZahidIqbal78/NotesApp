namespace NotesApp.API.DTOs.Note
{
    public class ReminderStatsResponseDto
    {
        public string NoteText { get; set; }
        public DateTime? ReminderOrDueDate { get; set; }
    }
}
