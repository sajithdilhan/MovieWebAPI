using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/user/login
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            var token = _userService.Login(user.UserName, user.Password);

            if (token == null || token == String.Empty)
                return BadRequest(new { message = "User name or password is incorrect" });

            return Ok(token);
        }
    }
}
