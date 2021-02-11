using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasks.DAL.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Required]
        [Range(18, 80, ErrorMessage = "Invalid age(18-80)")]
        public int Age { get; set; }

        public IEnumerable<AdditionalTaskEmployee> TaskEmployees { get; private set; }
    }
}
