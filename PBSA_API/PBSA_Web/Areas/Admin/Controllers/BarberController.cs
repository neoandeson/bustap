//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using DataService.Constants;
//using DataService.Models;
//using DataService.Services;
//using DataService.ViewModels;
//using Microsoft.AspNetCore.Mvc;

//namespace PBSA_Web.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class BarberController : Controller
//    {
//        private readonly IBarberService _barberService;
//        private readonly IMapper _mapper;

//        public BarberController(IBarberService barberService, IMapper mapper)
//        {
//            this._barberService = barberService;
//            this._mapper = mapper;
//        }

//        [Route("Admin/Barber")]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [Route("Admin/Barber/GetBarbers")]
//        public IActionResult GetBarbers()
//        {
//            List<Barbers> barbers = _barberService.GetAll();
//            List<BarberViewModel> lsBarberVM = new List<BarberViewModel>();
//            BarberViewModel barberVM;
//            foreach (var barber in barbers)
//            {
//                barberVM = new BarberViewModel();
//                barberVM.Address = barber.Address;
//                barberVM.ContactPhone = barber.ContactPhone;
//                barberVM.FullName = barber.GetFullName();
//                barberVM.OverallRate = barber.OverallRate;
//                barberVM.Username = barber.Username;
//                barberVM.IsActive = barber.IsActive;

//                lsBarberVM.Add(barberVM);
//            }

//            return new JsonResult(new
//            {
//                draw = 1,
//                recordsTotal = lsBarberVM.Count,
//                recordsFiltered = lsBarberVM.Count,
//                data = lsBarberVM
//            });
//        }

//        [Route("Admin/Barber/GetBarberInfo")]
//        public BarberDetailViewModel GetBarberInfo(string username)
//        {
//            Barbers barber = _barberService.GetBarberInfo(username);
//            BarberDetailViewModel barberDetailVM = new BarberDetailViewModel();
//            barberDetailVM.Address = barber.Address;
//            barberDetailVM.City = barber.CityCodeNavigation.Name;
//            barberDetailVM.ContactPhone = barber.ContactPhone;
//            barberDetailVM.District = barber.DistrictCodeNavigation.Name;
//            barberDetailVM.FullName = barber.GetFullName();
//            barberDetailVM.Latitude = barber.Latitude;
//            barberDetailVM.Longitude = barber.Longitude;
//            barberDetailVM.OverallRate = barber.OverallRate;
//            barberDetailVM.RatingCount = barber.RatingCount;
//            barberDetailVM.PaymentMethods = barber.User.PaymentMethods;
//            barberDetailVM.Username = barber.Username;
//            barberDetailVM.IsActive = barber.IsActive;
//            return barberDetailVM;
//        }

//        [Route("Admin/Barber/Save")]
//        public void Save(string username, bool isActive)
//        {
//            _barberService.UpdateBarberState(username, isActive);
//        }

//        //[Route("Admin/Barber/Save")]
//        //public void Save(BarberDetailViewModel barberDetailVM)
//        //{
//        //    Barbers barber = _barberService.GetBarberInfo(barberDetailVM.Username);
//        //    if (barber != null)
//        //    {
//        //        barber.Address = barberDetailVM.Address;
//        //        barber.CityCodeNavigation.Name = barberDetailVM.City;
//        //        barber.ContactPhone = barberDetailVM.ContactPhone;
//        //        barber.DistrictCodeNavigation.Name = barberDetailVM.District;
//        //        barber.FullName = barberDetailVM.FullName;
//        //        barber.Latitude = barberDetailVM.Latitude;
//        //        barber.Longitude = barberDetailVM.Longitude;
//        //        barber.OverallRate = barberDetailVM.OverallRate;
//        //        barber.RatingCount = barberDetailVM.RatingCount;
//        //        barber.User.PaymentMethods = barberDetailVM.PaymentMethods;
//        //        barber.IsActive = barberDetailVM.IsActive;
//        //        barber.Username = barberDetailVM.Username;
//        //    }
//        //    _barberService.Save(barber);
//        //}
//    }
//}