using System;
using System.Collections.Generic;
using System.Text;
using Tasks.DAL.Enums;

namespace Tasks.BLL.DTOs
{
    public class AdditionalTaskDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int EstimatedDuration { get; set; }
        public TaskComplexity Complexity { get; set; }
        public decimal Payment { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsFinished { get; set; }

        public IEnumerable<AdditionalTaskEmployeeDTO> TaskEmployees { get; set; }
    }
}
