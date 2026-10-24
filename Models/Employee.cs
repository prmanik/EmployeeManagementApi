using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApi.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Department { get; set; } = string.Empty;

        [Range(1, 10000000)]
        public decimal Salary { get; set; }

        [Range(0, 1000000)]
        public decimal? Bonus { get; set; }
    }
}