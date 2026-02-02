using AttendanceSystem.Application.DTOs;
using AttendanceSystem.Application.Interfaces;
using AttendanceSystem.Domain.Entities;
using AttendanceSystem.Domain.Enums;

namespace AttendanceSystem.Application.Services;

public class AttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AttendanceService(IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository)
    {
        _attendanceRepository = attendanceRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<AttendanceDto?> RecordPunchAsync(PunchRequest request)
    {
        var employee = await _employeeRepository.GetByCodeAsync(request.EmployeeCode);
        if (employee == null || !employee.IsActive)
            return null;

        var punchType = request.PunchType.ToUpper() == "IN" ? PunchType.In : PunchType.Out;

        var record = new AttendanceRecord
        {
            EmployeeId = employee.Id,
            PunchTime = request.PunchTime,
            PunchType = punchType,
            DeviceId = request.DeviceId
        };

        var saved = await _attendanceRepository.AddAsync(record);

        return new AttendanceDto(
            saved.Id,
            employee.Id,
            employee.Code,
            employee.Name,
            saved.PunchTime,
            saved.PunchType.ToString(),
            saved.DeviceId
        );
    }

    public async Task<IEnumerable<AttendanceDto>> GetAttendanceAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        var records = await _attendanceRepository.GetAllAsync(fromDate, toDate);
        return records.Select(r => new AttendanceDto(
            r.Id,
            r.EmployeeId,
            r.Employee.Code,
            r.Employee.Name,
            r.PunchTime,
            r.PunchType.ToString(),
            r.DeviceId
        ));
    }

    public async Task<DashboardStats> GetDashboardStatsAsync()
    {
        var totalEmployees = await _employeeRepository.GetTotalCountAsync();
        var presentToday = await _attendanceRepository.GetUniquePresentTodayCountAsync();
        var totalPunchesToday = await _attendanceRepository.GetTodayPunchCountAsync();

        return new DashboardStats(
            totalEmployees,
            presentToday,
            totalEmployees - presentToday,
            totalPunchesToday
        );
    }
}
