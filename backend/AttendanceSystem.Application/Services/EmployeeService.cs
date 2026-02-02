using AttendanceSystem.Application.DTOs;
using AttendanceSystem.Application.Interfaces;
using AttendanceSystem.Domain.Entities;

namespace AttendanceSystem.Application.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.Select(e => new EmployeeDto(e.Id, e.Code, e.Name, e.Department, e.IsActive));
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null) return null;
        return new EmployeeDto(employee.Id, employee.Code, employee.Name, employee.Department, employee.IsActive);
    }

    public async Task<EmployeeDto?> CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        var existing = await _employeeRepository.GetByCodeAsync(request.Code);
        if (existing != null) return null;

        var employee = new Employee
        {
            Code = request.Code,
            Name = request.Name,
            Department = request.Department
        };

        var saved = await _employeeRepository.AddAsync(employee);
        return new EmployeeDto(saved.Id, saved.Code, saved.Name, saved.Department, saved.IsActive);
    }

    public async Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeRequest request)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null) return null;

        employee.Name = request.Name;
        employee.Department = request.Department;
        employee.IsActive = request.IsActive;

        await _employeeRepository.UpdateAsync(employee);
        return new EmployeeDto(employee.Id, employee.Code, employee.Name, employee.Department, employee.IsActive);
    }
}
