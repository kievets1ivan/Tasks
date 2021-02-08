using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.BLL.Filters;
using Tasks.BLL.Services;

namespace Tasks.API.Controllers
{
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{emplyeeId}")]
        //GET: api/Employee/{emplyeeId}
        public async Task<IActionResult> GetById([FromRoute] int emplyeeId)
        {
            return Ok(await _employeeService.GetEmployeeById(emplyeeId));
        }
    }
}
