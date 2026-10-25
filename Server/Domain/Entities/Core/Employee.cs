using System.ComponentModel.DataAnnotations;

namespace Server.Domain.Entities.Core
{
    public class Employee: BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public Guid DepartmentId { get; set; }

        public string Gender { get; set; }
        public string Mobile { get; set; }
        public DateTime DOB { get; set; }
    }
}
