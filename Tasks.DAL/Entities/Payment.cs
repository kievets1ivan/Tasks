using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.DAL.Entities
{
    public class Payment : BaseEntity
    {
        public string FullName { get; set; }
        public string TaskTitle { get; set; }
        public decimal Reward { get; set; }
    }
}
