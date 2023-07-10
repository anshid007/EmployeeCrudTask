using System.ComponentModel.DataAnnotations;

namespace EmployeeCrudApi.Models
{
    public class Department
    {
        
        public int Id { get; set; }
        public string? Code { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public List<Employee>? employees { get; set;}
    }
}
