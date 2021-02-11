using System.ComponentModel.DataAnnotations;

namespace Tasks.DAL.Entities
{
    public class AdditionalTaskEmployee : BaseEntity
    {
        [Required]
        public int AdditionalTaskId { get; set; }
        [Required]
        public int EmployeeId { get; set; }

        public AdditionalTask AdditionalTask { get; set; }
        public Employee Employee { get; set; }
    }
}
