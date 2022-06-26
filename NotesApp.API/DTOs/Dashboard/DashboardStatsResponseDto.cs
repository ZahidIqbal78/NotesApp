using NotesApp.API.DTOs.Note;

namespace NotesApp.API.DTOs.Dashboard
{
    public class DashboardStatsResponseDto
    {
        public IEnumerable<TodoStatsResponseDto> Todos { get; set; } = new List<TodoStatsResponseDto>();
        public IEnumerable<ReminderStatsResponseDto> Reminders { get; set; } = new List<ReminderStatsResponseDto>();
    }
}
