using AttendanceSystem.Application.Interfaces;
using AttendanceSystem.Domain.Entities;
using AttendanceSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AttendanceDbContext _context;

    public AttendanceRepository(AttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AttendanceRecord>> GetAllAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        var query = _context.AttendanceRecords
            .Include(a => a.Employee)
            .AsQueryable();

        if (fromDate.HasValue)
            query = query.Where(a => a.PunchTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(a => a.PunchTime <= toDate.Value);

        return await query.OrderByDescending(a => a.PunchTime).ToListAsync();
    }

    public async Task<IEnumerable<AttendanceRecord>> GetByEmployeeIdAsync(int employeeId, DateTime? fromDate = null, DateTime? toDate = null)
    {
        var query = _context.AttendanceRecords
            .Include(a => a.Employee)
            .Where(a => a.EmployeeId == employeeId);

        if (fromDate.HasValue)
            query = query.Where(a => a.PunchTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(a => a.PunchTime <= toDate.Value);

        return await query.OrderByDescending(a => a.PunchTime).ToListAsync();
    }

    public async Task<AttendanceRecord> AddAsync(AttendanceRecord record)
    {
        _context.AttendanceRecords.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<int> GetTodayPunchCountAsync()
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);
        return await _context.AttendanceRecords
            .CountAsync(a => a.PunchTime >= today && a.PunchTime < tomorrow);
    }

    public async Task<int> GetUniquePresentTodayCountAsync()
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);
        return await _context.AttendanceRecords
            .Where(a => a.PunchTime >= today && a.PunchTime < tomorrow)
            .Select(a => a.EmployeeId)
            .Distinct()
            .CountAsync();
    }
}
