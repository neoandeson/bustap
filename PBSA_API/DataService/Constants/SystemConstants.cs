using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataService.Utils;

namespace DataService.Constants
{
    public static class SystemConstants
    {
        
    }

    public static class RoleConstants
    {
        public static int Driver = 1;
        public static int Customer = 2;
        public static int Manager = 3;
    }

    public static class ReturnState
    {
        public static string NotFound = "Không tìm thấy";
        public static string BeforeChangeSlotMinutes = "30";
        public static string CheckoutAcceptableLateMinute = "10";
        public static string CheckoutAcceptableViolationMinute = "10";
    }
}