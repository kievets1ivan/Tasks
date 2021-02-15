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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        //POST: api/User/Login
        public async Task<IActionResult> Login([FromBody] AuthDTO user)
        {
            return Ok(await _userService.Login(user));
        }
    }
}
