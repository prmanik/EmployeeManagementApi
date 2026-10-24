using EmployeeManagementApi.Models;

namespace EmployeeManagementApi.Services
{
    public interface IEmployeeService
    {
        Task<Employee?> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();

        Task<Employee> AddAsync(Employee employee);

        Task<bool> UpdateAsync(int id, Employee employee);

        Task<bool> DeleteAsync(int id);
    }

}