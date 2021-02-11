using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.DAL.Entities
{
    public class Check : BaseEntity
    {
        public ICollection<Payment> Payments { get; set; }
    }
}
