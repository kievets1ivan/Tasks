using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.BLL.DTOs
{
    public class EmployeeDTO : BaseEntityDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

        public IEnumerable<AdditionalTaskEmployeeDTO> TaskEmployees { get; set; }
    }
}
