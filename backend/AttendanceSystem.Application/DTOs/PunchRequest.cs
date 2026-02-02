namespace AttendanceSystem.Application.DTOs;

public record PunchRequest(
    string EmployeeCode,
    DateTime PunchTime,
    string DeviceId,
    string PunchType  // "IN" or "OUT"
);
