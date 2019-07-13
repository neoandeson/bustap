using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Constants;
using DataService.Services;
using DataService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BUSTAP_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IDPaperController : ControllerBase
    {
        private readonly IIDPaperService _idPaperService;

        public IDPaperController(IIDPaperService idPaperService)
        {
            _idPaperService = idPaperService;
        }

        [HttpPost("/idpp/register")]
        public IActionResult RegisterIDPaper(string content, string type, int customerId)
        {
            bool result = _idPaperService.RegisterIDPaper(type, content, customerId);
            if (result)
            {
                return new JsonResult(IDPaperConstants.REGISTER_SUCCESSFULL) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult(IDPaperConstants.REGISTER_FAIL) { StatusCode = StatusCodes.Status404NotFound };
        }

        [HttpPost("/idpp/delete")]
        public IActionResult Delete(int id)
        {
            bool result = _idPaperService.DeleteIDPaper(id);
            if (result)
            {
                return new JsonResult(IDPaperConstants.DELETE_SUCCESSFULL) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult(IDPaperConstants.DELETE_FAIL) { StatusCode = StatusCodes.Status404NotFound };
        }

        [HttpPost("/idpp/activate")]
        public IActionResult ActivateIDPaper(int id, bool state)
        {
            bool result = _idPaperService.ChangeState(id, state);
            if (result)
            {
                if (state)
                {
                    return new JsonResult(IDPaperConstants.ACTIVATED) { StatusCode = StatusCodes.Status200OK };
                }

                return new JsonResult(IDPaperConstants.DEACTIVATED) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult(IDPaperConstants.ERROR) { StatusCode = StatusCodes.Status404NotFound };
        }

        [HttpGet("/idpp/getall")]
        public IActionResult GetByUserId(int userId)
        {
            List<IDPaperInfo> iDPaperInfos = _idPaperService.GetByUserId(userId);

            return new JsonResult(iDPaperInfos) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost("/idpp/getUserInfo")]
        public IActionResult GetUserInfoByIDPaper(string content)
        {
            IDPaperInfo result = _idPaperService.GetUserByIDPaper(content);
            if (result != null)
            {
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult(result) { StatusCode = StatusCodes.Status404NotFound };
        }
    }
}