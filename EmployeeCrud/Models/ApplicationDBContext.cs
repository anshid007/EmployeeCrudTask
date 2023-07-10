using EmployeeCrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrud.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
