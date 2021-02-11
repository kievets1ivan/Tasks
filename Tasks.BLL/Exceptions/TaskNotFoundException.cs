using System;

namespace Tasks.BLL.Exceptions
{
    public class TaskNotFoundException : CustomException
    {
        private const string ExceptionMessage = "Task could not be found.";

        public TaskNotFoundException()
           : base(ExceptionMessage) { }

        public TaskNotFoundException(string message)
            : base(message) { }

        public TaskNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
