//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using DataService.Models;
//using DataService.Services;
//using DataService.ViewModels;
//using Microsoft.AspNetCore.Mvc;

//namespace PBSA_Web.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class TransactionController : Controller
//    {
//        private readonly ITransactionService _transactionService;

//        public TransactionController(ITransactionService transactionService)
//        {
//            this._transactionService = transactionService;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        [Route("Admin/Transaction/GetTransactions")]
//        public IActionResult GetTransactions()
//        {
//            List<Transactions> transactions = _transactionService.GetTransactions();
//            List<TransactionViewModel> lsTranVM = new List<TransactionViewModel>();
//            TransactionViewModel transactionVM;
//            foreach (var tran in transactions)
//            {
//                transactionVM = new TransactionViewModel();
//                transactionVM.Id = tran.Id;
//                transactionVM.SenderName = tran.SenderName;
//                transactionVM.ReceiverName = tran.ReceiverName;
//                transactionVM.State = tran.State;
//                transactionVM.Time = tran.Time;
//                lsTranVM.Add(transactionVM);
//            }

//            return new JsonResult(new
//            {
//                draw = 1,
//                recordsTotal = lsTranVM.Count,
//                recordsFiltered = lsTranVM.Count,
//                data = lsTranVM
//            });
//        }

//        [Route("Admin/Transaction/GetTransactionInfo")]
//        public TransactionViewModel GetTransactionInfo(int id)
//        {
//            Transactions tran = _transactionService.GetTransactionsInfo(id);
//            TransactionViewModel transactionVM = new TransactionViewModel();
//            transactionVM = new TransactionViewModel();
//            transactionVM.Type = tran.Type;
//            transactionVM.SenderName = tran.SenderName;
//            transactionVM.ReceiverName = tran.ReceiverName;
//            transactionVM.Time = tran.Time;
//            transactionVM.State = tran.State;
//            transactionVM.Detail = tran.Detail;
//            transactionVM.Amount = tran.Amount;
//            transactionVM.CurrencyCode = tran.CurrencyCode;
//            return transactionVM;
//        }
//    }
//}