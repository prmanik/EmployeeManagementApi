namespace EmployeeManagementApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees = new()
        {
            new Employee(1, "John Doe", "IT"),
            new Employee(2, "Jane Smith", "HR"),
            new Employee(3, "Michael Johnson", "Finance")
        };
        public Employee? GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }
    }
}