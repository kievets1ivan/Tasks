using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasks.DAL.Enums;

namespace Tasks.DAL.Entities
{
    public class AdditionalTask : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }
        public string Description { get; set; }
        public int EstimatedDuration { get; set; }
        [Required]
        public TaskComplexity Complexity { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Payment { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Start { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime End { get; set; }
        public bool IsFinished { get; set; }

        public IEnumerable<Employee> Employees { get; private set; }
    }
}
