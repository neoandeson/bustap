using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Models;
using DataService.Services;
using DataService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PBSA_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly IUserService _userService;

        public CustomerController(IUserService userService)
        {
            this._userService = userService;
        }

        [Route("Admin/Customer")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Admin/Customer/GetCustomers")]
        public IActionResult GetCustomers()
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
    }
}