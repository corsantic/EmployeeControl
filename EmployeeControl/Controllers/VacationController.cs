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
    public class VacationController : ControllerBase
    {
        private IVacationService _service;

        public VacationController(IVacationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.Name).Value);
                var roleId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.Role).Value);

                var res = await _service.GetAsync(userId, roleId);

                return Ok(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(VacationRequestParameter vacationRequestParameter)
        {
            try
            {
                var res = await _service.ChangeStatusAsync(vacationRequestParameter);
                return Ok(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}