//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using DataService.Models;
//using DataService.Services;
//using DataService.ViewModels;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using static DataService.Constants.ComplaintConstants;

//namespace PBSA_Web.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class HairController : Controller
//    {
//        private readonly IHairStyleService _hairStyleService;
//        private readonly IFaceShapeService _faceShapeService;
//        private readonly IHairStyleSampleService _hairStyleSampleService;

//        public HairController(IHairStyleService hairStyleService, IFaceShapeService faceShapeService, IHairStyleSampleService hairStyleSampleService)
//        {
//            _hairStyleService = hairStyleService;
//            _faceShapeService = faceShapeService;
//            _hairStyleSampleService = hairStyleSampleService;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Detail(int id)
//        {
//            return View(id);
//        }

//        [Route("Admin/HairStyle/GetHairStyles")]
//        public IActionResult GetAllTable()
//        {
//            List<HairStyleViewModel> hairStyleVMs = new List<HairStyleViewModel>();
//            HairStyleViewModel hairStyleVM = null;
//            List<HairStyles> hairStyles = _hairStyleService.GetAll();
//            foreach (var h in hairStyles)
//            {
//                hairStyleVM = new HairStyleViewModel()
//                {
//                    Id = h.Id,
//                    Description = h.Description,
//                    DifficultyRate = h.DifficultyRate,
//                    HairStyleName = h.HairStyleName,
//                    ViewedCount = h.ViewedCount
//                };

//                hairStyleVMs.Add(hairStyleVM);
//            }
//            if (hairStyleVMs != null)
//            {
//                return new JsonResult(new
//                {
//                    draw = 1,
//                    recordsTotal = hairStyleVMs.Count,
//                    recordsFiltered = hairStyleVMs.Count,
//                    data = hairStyleVMs
//                });
//            }
//            return new JsonResult(ReturnState.NotFound) { StatusCode = StatusCodes.Status404NotFound };
//        }

//        [Route("Admin/HairStyle/GetHairStyleInfo")]
//        public IActionResult GetHairStyleInfo(int id)
//        {
//            //HairStyleViewModel hairStyleVM = null;
//            HairStyles h = _hairStyleService.GetHairStyleById(id);
//            //hairStyleVM = new HairStyleViewModel()
//            //{
//            //    Id = h.Id,
//            //    Description = h.Description,
//            //    DifficultyRate = h.DifficultyRate,
//            //    HairStyleName = h.HairStyleName,
//            //    ViewedCount = h.ViewedCount
//            //};

//            if (h != null)
//            {
//                return new JsonResult(h) { StatusCode = StatusCodes.Status200OK };
//            }
//            return new JsonResult(ReturnState.NotFound) { StatusCode = StatusCodes.Status404NotFound };
//        }

//        [Route("Admin/HairStyle/GetFaces")]
//        public IActionResult GetFaces()
//        {
//            List<FaceShapes> faceShapes = _faceShapeService.GetFaceShapes();

//            if (faceShapes != null)
//            {
//                return new JsonResult(faceShapes) { StatusCode = StatusCodes.Status200OK };
//            }
//            return new JsonResult(ReturnState.NotFound) { StatusCode = StatusCodes.Status404NotFound };
//        }

//        [Route("Admin/HairStyle/AddHairSample")]
//        public IActionResult AddHairSample(HairStyleSampleViewModel hsSampleVM)
//        {
//            bool result = _hairStyleSampleService.AddHairStyleSample(hsSampleVM);

//            if (result)
//            {
//                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
//            }
//            return new JsonResult(ReturnState.NotFound) { StatusCode = StatusCodes.Status404NotFound };
//        }
//    }
//}