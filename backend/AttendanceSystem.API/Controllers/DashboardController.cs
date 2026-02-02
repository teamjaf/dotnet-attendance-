using AttendanceSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AttendanceService _attendanceService;

    public DashboardController(AttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var stats = await _attendanceService.GetDashboardStatsAsync();
        return Ok(stats);
    }
}
