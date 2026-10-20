namespace EmployeeManagementApi.Services
{
    public interface IEmployeeService
    {
        Employee? GetById(int id);
        IEnumerable<Employee> GetAll();

        Employee Add(Employee employee);

        bool Update(int id, Employee employee);

        bool Delete(int id);
    }
    
}