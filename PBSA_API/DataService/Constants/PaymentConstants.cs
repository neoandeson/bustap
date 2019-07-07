using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Constants
{
    public static class PaymentConstants
    {
        
        public static readonly int MINIMUN_BALANCE = 0;

        public static readonly string NOT_FOUND_WALLET = "Không tìm thấy ví";
        public static readonly string INVALID_BALANCE = "Số dư không khả dụng";
        public static readonly string VALID_BALANCE = "Số dư hợp lệ";

        public static readonly string PAYMENT_TYPE_PAY = "Thanh toán";
        public static readonly string PAYMENT_TYPE_TOPUP = "Nạp tiền";
        public static readonly string PAYMENT_STATE_SUCCESS = "Hoàn thành";

        public static readonly string TRANS_SUCESSFULL = "Giao dịch thành công";
        public static readonly string TRANS_FAIL = "Giao dịch thất bại";
    }
}
