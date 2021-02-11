using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.BLL.DTOs
{
    public class PaymentDTO : BaseEntityDTO
    {
        public string FullName { get; set; }
        public string TaskTitle { get; set; }
        public decimal Reward  { get; set; }
    }
}
