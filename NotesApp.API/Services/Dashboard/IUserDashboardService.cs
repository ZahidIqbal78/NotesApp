using NotesApp.API.DTOs.Dashboard;

namespace NotesApp.API.Services.Dashboard
{
    public interface IUserDashboardService
    {
        DashboardStatsResponseDto GetTodayStats(int applicationUserId);
        DashboardStatsResponseDto GetNextSevenDaysStats(int applicationUserId);
        DashboardStatsResponseDto GetNextMonthStats(int applicationUserId);
    }
}
