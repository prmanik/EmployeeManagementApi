using EmployeeManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees => Set<Employee>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Department).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Salary).IsRequired().HasPrecision(18, 2);
                entity.Property(e => e.Bonus).IsRequired(false).HasPrecision(18, 2);
                entity.HasData(
                    new Employee { Id = 1, Name = "John Doe", Department = "HR", Salary = 50000 },
                    new Employee { Id = 2, Name = "Jane Smith", Department = "IT", Salary = 60000 },
                    new Employee { Id = 3, Name = "Mike Johnson", Department = "Finance", Salary = 55000, Bonus = 5000 }
                );
            });
        }
    }
}