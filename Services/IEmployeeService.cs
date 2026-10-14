namespace EmployeeManagementApi.Services
{
    public interface IEmployeeService
    {
        Employee? GetById(int id);
        IEnumerable<Employee> GetAll();
    }

    public record Employee(int Id, string Name, string Department);
}