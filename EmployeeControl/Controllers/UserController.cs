using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeContol.model;
using EmployeeContol.service;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeControl.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserParameter userParameter)
        {
            try
            {
                var user = await _service.AuthenticateAsync(userParameter.UserName, userParameter.Password);
                if (user != null) return Ok(user);
                // Log.Warning($"Bad request {userParameter.UserName}");
                return BadRequest(new {message = "Username or password is incorrect"});
            }
            catch (Exception e)
            {
                // Log.Error(e, $"Authenticate error {userParameter.UserName}");
                Console.WriteLine(DateTime.Now + "-" + e);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.Name).Value);
                var res = await _service.GetAsync(userId);
                
                return Ok(res);
            }
            catch (Exception e)
            {
                // Log.Error(e, $"Authenticate error {userParameter.UserName}");
                Console.WriteLine(DateTime.Now + "-" + e);
                throw;
            }
        }
    }
}