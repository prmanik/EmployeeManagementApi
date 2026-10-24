using System.Collections.Concurrent;
using System.Data;
using EmployeeManagementApi.Data;
using EmployeeManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApi.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly AppDbContext _dbContext;

        public EmployeeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees.AsNoTracking().OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            employee.Id = 0; // Ensure the Id is set to 0 so that EF Core generates a new Id
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateAsync(int id, Employee employee)
        {
            var existingEmployee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (existingEmployee == null)
            {
                return false;
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Department = employee.Department;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.Bonus = employee.Bonus;
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        => await _dbContext.Employees.Where(e => e.Id == id).ExecuteDeleteAsync() > 0;

    }
}