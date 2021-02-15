using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.BLL.DTOs;
using Tasks.BLL.Filters;
using Tasks.BLL.Services;

namespace Tasks.API.Controllers
{
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{employeeId}")]
        //GET: api/Employee/{employeeId}
        public async Task<IActionResult> GetById([FromRoute] int employeeId)
        {
            return Ok(await _employeeService.GetEmployeeById(employeeId));
        }

        [HttpGet]
        //GET: api/Employee
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetAll());
        }

        [HttpPost]
        //POST: api/Employee
        public async Task<IActionResult> Create([FromBody] EmployeeDTO employeeDTO)
        {
            return Ok(await _employeeService.AddEmployee(employeeDTO));
        }

        [HttpPut]
        //PUT: api/Employee
        public async Task<IActionResult> Update([FromBody] EmployeeDTO employeeDTO)
        {
            return Ok(await _employeeService.UpdateEmployee(employeeDTO));
        }

        [HttpDelete("{employeeId}")]
        //DELETE: api/Employee/{employeeId}
        public async Task<IActionResult> Delete([FromRoute] int employeeId)
        {
            return Ok(await _employeeService.DeleteEmployeeById(employeeId));
        }
    }
}
