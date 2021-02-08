using System;

namespace Tasks.BLL.Exceptions
{
    public class EmployeeNotFoundException : CustomException
    {
        private const string ExceptionMessage = "Employee could not be found.";

        public EmployeeNotFoundException()
           : base(ExceptionMessage) { }

        public EmployeeNotFoundException(string message)
            : base(message) { }

        public EmployeeNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
