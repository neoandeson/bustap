using DataService.ViewModels;
using DataService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PBSA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;

        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/login")]
        public IActionResult Login(string username, string password, string roleName)
        {
            bool result = _userService.Authenticate(username, password, roleName);
            if (result)
            {
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult("Thông tin đăng nhập không hợp lệ") { StatusCode = StatusCodes.Status409Conflict };
        }

        [HttpPost("/register")]
        public IActionResult Register(AuthUser authUser)
        {
            var result = _userService.Register(authUser);
            if (result)
            {
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult("Thông tin đăng ký không hợp lệ") { StatusCode = StatusCodes.Status409Conflict };
        }
    }
}