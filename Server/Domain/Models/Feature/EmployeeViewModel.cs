namespace Server.Domain.Models.Feature
{
    public class EmployeeViewModel
    {
        //public int Id { get; set; }
        public Guid RecordId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Mobile { get; set; }
        public int Salary { get; set; }
        public Guid DepartmentId { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
