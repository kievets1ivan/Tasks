using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.BLL.Exceptions
{
    public class CheckNotFoundException : CustomException
    {
        private const string ExceptionMessage = "Check could not be found.";

        public CheckNotFoundException()
           : base(ExceptionMessage) { }

        public CheckNotFoundException(string message)
            : base(message) { }

        public CheckNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
