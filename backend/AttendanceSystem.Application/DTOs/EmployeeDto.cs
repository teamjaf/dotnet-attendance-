namespace AttendanceSystem.Application.DTOs;

public record EmployeeDto(
    int Id,
    string Code,
    string Name,
    string Department,
    bool IsActive
);

public record CreateEmployeeRequest(
    string Code,
    string Name,
    string Department
);

public record UpdateEmployeeRequest(
    string Name,
    string Department,
    bool IsActive
);
