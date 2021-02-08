using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tasks.BLL.Exceptions;

namespace Tasks.BLL.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter, IFilterMetadata
    {

        public CustomExceptionFilterAttribute()
        {

        }

        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext?.ExceptionHandled ?? false && exceptionContext.Exception is CustomException)
            {
                exceptionContext.Result = new BadRequestObjectResult($"{exceptionContext.Exception.Message} - {exceptionContext.Exception.GetType()}");
                exceptionContext.ExceptionHandled = true;
            }
            if (!exceptionContext?.ExceptionHandled ?? false && exceptionContext.Exception is Exception)
            {
                exceptionContext.Result = new BadRequestObjectResult($"{exceptionContext.Exception.Message}");
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}
