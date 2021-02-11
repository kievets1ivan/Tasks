using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.BLL.DTOs;
using Tasks.BLL.Filters;
using Tasks.BLL.Models;
using Tasks.BLL.Services;

namespace Tasks.API.Controllers
{
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{taskId}")]
        //GET: api/Task/{taskId}
        public async Task<IActionResult> GetById([FromRoute] int taskId)
        {
            return Ok(await _taskService.GetTaskById(taskId));
        }

        [HttpGet]
        //GET: api/Task
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _taskService.GetAll());
        }

        [HttpPost]
        //POST: api/Task
        public async Task<IActionResult> Create([FromBody] AdditionalTaskDTO taskDTO)
        {
            return Ok(await _taskService.AddTask(taskDTO));
        }

        [HttpPut]
        //PUT: api/Task
        public async Task<IActionResult> Update([FromBody] AdditionalTaskDTO taskDTO)
        {
            return Ok(await _taskService.UpdateTask(taskDTO));
        }

        [HttpDelete("{taskId}")]
        //DELETE: api/Task/{taskId}
        public async Task<IActionResult> Delete([FromRoute] int taskId)
        {
            return Ok(await _taskService.DeleteTaskById(taskId));
        }


        [HttpPut("do")]
        //PUT: api/Task/do
        public async Task<IActionResult> DoTasks([FromBody] IEnumerable<int> tasksIds, [FromQuery] TaskInputParameters parameters)
        {
            return Ok(await _taskService.DoTasks(tasksIds, parameters));
        }
    }
}
