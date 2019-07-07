using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataService.Utils
{
    public static class StringUtil
    {
        // http://chiencong.com/chuyen-tieng-viet-co-dau-thanh-khong-dau-trong-c-javascript-va-mssql-server
        public static string ConvertToUnSignUnicode(this string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        
        public static string NormalizeLower(this string s)
        {
            return s.ConvertToUnSignUnicode().ToLower().Trim();
        }

        public static string RemoveSubstring(this string source, params string[] s)
        {
            var tmp = source;
            foreach (var str in s)
            {
                tmp = tmp.Replace(str, "");
            }

            return tmp;
        }

        public static string TrimOrEmpty(this string source)
        {
            return source == null ? "" : source.Trim();
        }

        public static double Double(this string s)
        {
            return double.Parse(s);
        }
        
        public static float Float(this string s)
        {
            return float.Parse(s);
        }
        
        public static int GetInt(this string s)
        {
            return int.Parse(s);
        }

        public static long GetLong(this string s)
        {
            return long.Parse(s);
        }
        
        public static decimal Decimal(this string s)
        {
            return decimal.Parse(s);
        }

        public static HashSet<string> ToDataSet(this string source)
        {
            if (source == null || source.Trim() == "")
            {
                return new HashSet<string>();
            }

            var set = source.Split(",").ToList()
                .Where(item => item != null && item.Trim() != "")
                .ToHashSet();

            return set;
        }

        public static string JoinToString(this HashSet<string> set)
        {
            if (set == null || set.Count == 0)
            {
                return "";
            }

            set = set
                .Where(item => item != null && item.Trim() != "")
                .ToHashSet();

            return string.Join(",", set);
        }

        public static string CapitalizeFirst(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;

            var firstChar = char.ToUpper(source[0]);

            if (source.Length == 1)
            {
                return firstChar.ToString();
            }

            return firstChar + source.Substring(1);
        }
    }
}