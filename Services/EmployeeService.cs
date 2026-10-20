using System.Collections.Concurrent;

namespace EmployeeManagementApi.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly ConcurrentDictionary<int, Employee> _employees
        = new();
        private int _lastId = 0;

        public EmployeeService()
        {
            // Initialize with some sample data
            Add(new Employee { Name = "John Doe", Department = "HR", Salary = 50000 });
            Add(new Employee { Name = "Jane Smith", Department = "IT", Salary = 60000 });
            Add(new Employee { Name = "Mike Johnson", Department = "Finance", Salary = 55000 });
        }
        public Employee? GetById(int id)
        {
            return _employees.TryGetValue(id, out Employee? employee) ? employee : null;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _employees.Values.OrderBy(e => e.Id);
        }

        public Employee Add(Employee employee)
        {
            employee.Id = Interlocked.Increment(ref _lastId);
            _employees.TryAdd(employee.Id, employee);
            return employee;
        }

        public bool Update(int id, Employee employee)
        {
            while (_employees.TryGetValue(id, out Employee? existingEmployee))
            {
                if (_employees.TryUpdate(id, employee, existingEmployee))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            return _employees.TryRemove(id, out _);
        }
    }
}