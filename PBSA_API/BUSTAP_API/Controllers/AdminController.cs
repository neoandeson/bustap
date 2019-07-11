using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Models;
using DataService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BUSTAP_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllDriver()
        {
            List<User> users = _userService.GetAllDrivers();
            return new JsonResult(new
            {
                draw = 1,
                recordsTotal = users.Count,
                recordsFiltered = users.Count,
                data = users
            });
        }

        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            List<User> users = _userService.GetAllCustomers();
            return new JsonResult(new
            {
                draw = 1,
                recordsTotal = users.Count,
                recordsFiltered = users.Count,
                data = users
            });
        }

        [HttpPost]
        public IActionResult ChangeState(int userId, bool isActivated)
        {
            bool result = _userService.ChangeState(userId, isActivated);
            if (result)
            {
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult(result) { StatusCode = StatusCodes.Status404NotFound };
        }
    }
}