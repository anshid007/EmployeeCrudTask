using System.ComponentModel.DataAnnotations;

namespace EmployeeUI.Dto
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }

    }
}
