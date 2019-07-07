using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PBSA_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SysVariableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}