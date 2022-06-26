using AutoMapper;
using NotesApp.API.Data;
using NotesApp.API.DTOs.Dashboard;
using NotesApp.API.DTOs.Note;

namespace NotesApp.API.Services.Dashboard
{
    public class UserDashboardService : IUserDashboardService
    {
        private readonly NotesAppDbContext context;
        private readonly IMapper mapper;
        public UserDashboardService(NotesAppDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public DashboardStatsResponseDto GetNextMonthStats(int applicationUserId)
        {
            var nextMonth = DateTime.Today.AddMonths(1);
            var result = this.context.Notes
                .Where(x => x.ApplicationUserId == applicationUserId &&
                    x.ReminderOrDueDate != null &&
                    x.ReminderOrDueDate.GetValueOrDefault().Month == nextMonth.Month &&
                    x.ReminderOrDueDate.GetValueOrDefault().Year == nextMonth.Year);

            DashboardStatsResponseDto response = new DashboardStatsResponseDto();

            if (result.Any())
            {
                response.Todos = this.mapper.Map<IEnumerable<TodoStatsResponseDto>>(result.Where(x => x.NoteType == "Todo").AsEnumerable());
                response.Reminders = this.mapper.Map<IEnumerable<ReminderStatsResponseDto>>(result.Where(x => x.NoteType == "Reminder").AsEnumerable());
            }

            return response;
        }

        public DashboardStatsResponseDto GetNextSevenDaysStats(int applicationUserId)
        {
            var nextWeekDateTime = this.GetFirstAndLastDateOfNextWeek();
            var result = this.context.Notes
                .Where(x => x.ApplicationUserId == applicationUserId &&
                    x.ReminderOrDueDate != null &&
                    x.ReminderOrDueDate.GetValueOrDefault() >= nextWeekDateTime.firstDateOfNextWeek &&
                    x.ReminderOrDueDate.GetValueOrDefault() <= nextWeekDateTime.lastDateOfNextWeek);
            
            DashboardStatsResponseDto response = new DashboardStatsResponseDto();

            if (result.Any())
            {
                response.Todos = this.mapper.Map<IEnumerable<TodoStatsResponseDto>>(result.Where(x => x.NoteType == "Todo").AsEnumerable());
                response.Reminders = this.mapper.Map<IEnumerable<ReminderStatsResponseDto>>(result.Where(x => x.NoteType == "Reminder").AsEnumerable());
            }

            return response;
        }

        public DashboardStatsResponseDto GetTodayStats(int applicationUserId)
        {
            var result = this.context.Notes
                .Where(x => x.ApplicationUserId == applicationUserId &&
                    x.ReminderOrDueDate != null &&
                    x.ReminderOrDueDate.GetValueOrDefault().Date == DateTime.Today);
            
            DashboardStatsResponseDto response = new DashboardStatsResponseDto();

            if (result.Any())
            {
                response.Todos = this.mapper.Map<IEnumerable<TodoStatsResponseDto>>(result.Where(x => x.NoteType == "Todo").AsEnumerable());
                response.Reminders = this.mapper.Map<IEnumerable<ReminderStatsResponseDto>>(result.Where(x => x.NoteType == "Reminder").AsEnumerable());
            }

            return response;
        }

        /// <summary>
        /// Gets the first and last day of next week from Today's date.
        /// Depends on the system date format. My system has Sunday 
        /// as the first day of the week.
        /// </summary>
        /// <returns>(DateTime, DateTime)</returns>
        private (DateTime firstDateOfNextWeek, DateTime lastDateOfNextWeek) GetFirstAndLastDateOfNextWeek()
        {
            var todayDayOfWeekInNumber = (int)DateTime.Today.DayOfWeek;
            var firstDateTimeOfNextWeek = DateTime.Today.AddDays(7 - (todayDayOfWeekInNumber));
            
            var firstDayOfNextWeek = new DateTime(
                firstDateTimeOfNextWeek.Year, 
                firstDateTimeOfNextWeek.Month, 
                firstDateTimeOfNextWeek.Day,
                0, 0, 0);

            var lastDayOfNextWeek = firstDayOfNextWeek.AddDays(6).AddHours(12).AddMinutes(59).AddSeconds(59);
            return(firstDayOfNextWeek, lastDayOfNextWeek);
        }
    }
}
