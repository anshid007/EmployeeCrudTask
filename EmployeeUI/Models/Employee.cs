using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCrudApi.Models
{
    public class Employee
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Gender { get; set; }

        public int? DepartmentId { get; set; }
        public Department? department { get; set; }
    }
}
