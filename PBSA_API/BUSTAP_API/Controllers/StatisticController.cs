using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Constants;
using DataService.Services;
using DataService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PBSA_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet("/statistic/user")]
        public IActionResult GetGeneralStatistic(int userId, int month)
        {
            var result = _statisticService.GetUserMonthSTT(userId, month);
            if (result != null)
            {
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
        }

        [HttpGet("/statistic/company")]
        public IActionResult GetMonthStatistic(int year, int month)
        {
            DateTimeOffset d = new DateTimeOffset(year, month, 1, 0, 0, 0, new TimeSpan(0, 0, 0));

            //MonthSTTViewModel monthSTTVM = _statisticService.GetMonthSTT(d);
            //if (monthSTTVM != null)
            //{
            //    return new JsonResult(monthSTTVM) { StatusCode = StatusCodes.Status200OK };
            //}
            return new JsonResult(ReturnState.NotFound) { StatusCode = StatusCodes.Status404NotFound };
        }
    }
}