using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.BLL.DTOs
{
    public class CheckDTO : BaseEntityDTO
    {
        public ICollection<PaymentDTO> Payments { get; set; }
        public decimal TotalSumOfRewards => Payments.Sum(x => x.Reward);
    }
}
