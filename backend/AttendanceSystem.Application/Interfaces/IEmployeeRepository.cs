using AttendanceSystem.Domain.Entities;

namespace AttendanceSystem.Application.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee?> GetByCodeAsync(string code);
    Task<Employee> AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task<int> GetTotalCountAsync();
}
