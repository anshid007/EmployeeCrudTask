namespace EmployeeUI.Dto
{
    public class UpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }

        public int? DepartmentId { get; set; }
    }
}
