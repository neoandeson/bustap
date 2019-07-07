using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Services;
using DataService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PBSA_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            this._userService = userService;
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