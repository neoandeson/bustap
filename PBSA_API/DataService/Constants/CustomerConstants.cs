using System.Collections.Generic;

namespace DataService.Constants
{
    public static class CustomerConstants
    {
        public static Dictionary<string, object> CustomerAccounts = new Dictionary<string, object>();

        public static class CustomerSettingName
        {
            public static string FavoriteBarbers = "FavoriteBarbers";
            public static string UnreachableBarbers = "UnreachableBarbers";
            public static string LastHaircutInUnixTime = "LastHaircutInUnixTime";
            public static string ReminderDays = "ReminderDays";
        }
        
        public static class CustomerSettingDefaultValue
        {
            public static string ReminderDays = "30";
        }
    }
}