using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly ICheckService _checkService;

        public CheckController(ICheckService checkService)
        {
            _checkService = checkService;
        }

        [HttpGet("{checkId}")]
        //GET: api/Check/{checkId}
        public async Task<IActionResult> GetById([FromRoute] int checkId)
        {
            return Ok(await _checkService.GetCheckById(checkId));
        }

        [HttpGet]
        //GET: api/Check
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _checkService.GetAll());
        }

        [HttpPost]
        //POST: api/Check
        public async Task<IActionResult> Post([FromBody] IEnumerable<int> tasksIds)
        {
            return Ok(await _checkService.AddCheck(tasksIds));
        }
    }
}
