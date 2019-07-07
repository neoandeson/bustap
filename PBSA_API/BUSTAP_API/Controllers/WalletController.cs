using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Constants;
using DataService.Models;
using DataService.Services;
using DataService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PBSA_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IIDPaperService _iDPaperService;

        public WalletController(IWalletService walletService, IIDPaperService iDPaperService)
        {
            _walletService = walletService;
            _iDPaperService = iDPaperService;
        }

        [HttpGet("/wallet")]
        public IActionResult GetWallet(int userId)
        {
            UserWalletInfo uwi = _walletService.GetWallet(userId);
            if (uwi != null)
            {
                return new JsonResult(uwi) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult(PaymentConstants.NOT_FOUND_WALLET) { StatusCode = StatusCodes.Status404NotFound };
        }

        [HttpPost("/wallet/payable")]
        public IActionResult CheckPayable(int userId, decimal amount)
        {
            bool isPayable = _walletService.CheckPayable(userId, amount);
            if (isPayable)
            {
                return new JsonResult(PaymentConstants.VALID_BALANCE) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult(PaymentConstants.INVALID_BALANCE) { StatusCode = StatusCodes.Status404NotFound };
        }

        [HttpPost("/wallet/pay-transaction")]
        public IActionResult PayTransaction(int senderId, int receiverId, decimal amount, string transDetail)
        {
            bool isPayable = _walletService.CheckPayable(senderId, amount);
            if (!isPayable)
            {
                return new JsonResult(PaymentConstants.INVALID_BALANCE) { StatusCode = StatusCodes.Status404NotFound };
            }

            var transaction = _walletService.PayTransaction(senderId, receiverId, amount, transDetail);
            if (transaction == null)
            {
                return new JsonResult(PaymentConstants.TRANS_FAIL) { StatusCode = StatusCodes.Status404NotFound };
            }
            return new JsonResult(PaymentConstants.TRANS_SUCESSFULL) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost("/wallet/pay-ticket")]
        public IActionResult PayTicket(int receiverId, string transDetail, string input)
        {
            IDPaperInfo iDPaperInfo = _iDPaperService.VerifyIDPaper(input);

            if(iDPaperInfo.Type == IDPaperConstants.IDPP_INVALID)
            {
                return new JsonResult(IDPaperConstants.IDPP_INVALID) { StatusCode = StatusCodes.Status404NotFound };
            }

            if (iDPaperInfo.Type == IDPaperConstants.IDPP_INACTIVE)
            {
                return new JsonResult(IDPaperConstants.IDPP_INACTIVE) { StatusCode = StatusCodes.Status404NotFound };
            }

            if (iDPaperInfo != null)
            {
                decimal amount = iDPaperInfo.AppliedPrice;
                bool isPayable = _walletService.CheckPayable(iDPaperInfo.UserId, amount);
                if (!isPayable)
                {
                    return new JsonResult(PaymentConstants.INVALID_BALANCE) { StatusCode = StatusCodes.Status404NotFound };
                }

                var transaction = _walletService.PayTransaction(iDPaperInfo.UserId, receiverId, amount, transDetail);
                if (transaction == null)
                {
                    return new JsonResult(PaymentConstants.TRANS_FAIL) { StatusCode = StatusCodes.Status404NotFound };
                }
                return new JsonResult(PaymentConstants.TRANS_SUCESSFULL) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(PaymentConstants.TRANS_FAIL) { StatusCode = StatusCodes.Status404NotFound };
        }

        [HttpPost("/wallet/topup-transaction")]
        public IActionResult TopupTransaction(int receiverId, decimal amount,
            string transDetail)
        {
            var transaction = _walletService.TopupTransaction(receiverId, amount, transDetail);
            if (transaction != null)
            {
                return new JsonResult(PaymentConstants.TRANS_SUCESSFULL) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult(PaymentConstants.NOT_FOUND_WALLET) { StatusCode = StatusCodes.Status404NotFound };
        }
    }

    //[HttpPost("/wallet/complete")]
    //public IActionResult CompleteTransaction(int transactionId)
    //{
    //    string result = _walletService.CompleteTransaction(transactionId);

    //    JsonResult jsonResult;

    //    if (result == PaymentDescription.Success)
    //    {
    //        jsonResult = new JsonResult(PaymentDescription.Success_VN) { StatusCode = StatusCodes.Status200OK };
    //    }
    //    else if (result == PaymentDescription.NotFoundTransaction)
    //    {
    //        jsonResult = new JsonResult(PaymentDescription.NotFoundTransaction_VN) { StatusCode = StatusCodes.Status404NotFound };
    //    }
    //    else
    //    {
    //        jsonResult = new JsonResult(PaymentDescription.Fail_VN) { StatusCode = StatusCodes.Status409Conflict };
    //    }

    //    return jsonResult;
    //}
}