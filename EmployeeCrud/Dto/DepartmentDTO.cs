using System.ComponentModel.DataAnnotations;

namespace EmployeeCrud.Dto
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }

        
    }
}
