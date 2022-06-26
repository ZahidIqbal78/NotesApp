using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.API.DomainModels;
using NotesApp.API.DTOs.Dashboard;
using NotesApp.API.Helpers;
using NotesApp.API.Services.Dashboard;

namespace NotesApp.API.Controllers
{
    [AuthorizeAttributeHelper]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserDashboardService userDashboardService;

        public DashboardController(IUserDashboardService userDashboardService,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.userDashboardService = userDashboardService;
        }

        [HttpGet]
        [Route("today")]
        public ActionResult<DashboardStatsResponseDto> GetToday()
        {
            var userId = (int)HttpContext.Items["UserId"];
            return Ok(this.userDashboardService.GetTodayStats(userId));
        }

        [HttpGet]
        [Route("nextseven")]
        public ActionResult<DashboardStatsResponseDto> GetNextSevenDays()
        {
            var userId = (int)HttpContext.Items["UserId"];
            return Ok(this.userDashboardService.GetNextSevenDaysStats(userId));
        }

        [HttpGet]
        [Route("nextmonth")]
        public ActionResult<DashboardStatsResponseDto> GetNextMonth()
        {
            var userId = (int)HttpContext.Items["UserId"];
            return Ok(this.userDashboardService.GetNextMonthStats(userId));
        }
    }
}
