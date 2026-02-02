namespace AttendanceSystem.Application.DTOs;

public record AttendanceDto(
    int Id,
    int EmployeeId,
    string EmployeeCode,
    string EmployeeName,
    DateTime PunchTime,
    string PunchType,
    string DeviceId
);

public record DashboardStats(
    int TotalEmployees,
    int PresentToday,
    int AbsentToday,
    int TotalPunchesToday
);
