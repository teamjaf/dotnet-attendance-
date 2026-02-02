using AttendanceSystem.Domain.Entities;

namespace AttendanceSystem.Application.Interfaces;

public interface IAttendanceRepository
{
    Task<IEnumerable<AttendanceRecord>> GetAllAsync(DateTime? fromDate = null, DateTime? toDate = null);
    Task<IEnumerable<AttendanceRecord>> GetByEmployeeIdAsync(int employeeId, DateTime? fromDate = null, DateTime? toDate = null);
    Task<AttendanceRecord> AddAsync(AttendanceRecord record);
    Task<int> GetTodayPunchCountAsync();
    Task<int> GetUniquePresentTodayCountAsync();
}
