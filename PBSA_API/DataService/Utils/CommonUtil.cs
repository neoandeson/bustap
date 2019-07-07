using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace DataService.Utils
{
    public static class CommonUtil
    {
        public static int RoundNumber(this int n, int roundedNumber)
        {
            int remainder = n % roundedNumber;
            if (remainder != 0)
            {
                n += roundedNumber - remainder;
            }
            return n;
        }

        public static string PaddingZero(this int n, int numberOfDigits)
        {
            var rs = n.ToString();
            var toPadding = numberOfDigits - rs.Length;
            if (toPadding > 0)
            {
                for (var i = 0; i < toPadding; i++)
                {
                    rs = "0" + rs;
                }
            }

            return rs;
        }

        public static Dictionary<TKey, List<TValue>> ClassifyToDictionary<TKey, TValue>(
            this List<TValue> source,
            Func<TValue, TKey> keySelector)
        {
            return source.GroupBy(keySelector)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.ToList());
        }
        
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}