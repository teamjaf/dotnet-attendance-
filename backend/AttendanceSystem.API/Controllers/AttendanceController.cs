using AttendanceSystem.Application.DTOs;
using AttendanceSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly AttendanceService _attendanceService;

    public AttendanceController(AttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    /// <summary>
    /// Endpoint for biometric machines to send punch data
    /// </summary>
    [HttpPost("punch")]
    public async Task<IActionResult> Punch([FromBody] PunchRequest request)
    {
        var result = await _attendanceService.RecordPunchAsync(request);
        if (result == null)
            return BadRequest(new { message = "Invalid employee code or employee is inactive" });

        return Ok(result);
    }

    /// <summary>
    /// Get all attendance records with optional date filters
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAttendance([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
    {
        var records = await _attendanceService.GetAttendanceAsync(fromDate, toDate);
        return Ok(records);
    }
}
