using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;

using System.Net.Mail;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.IO;
using System.ComponentModel.DataAnnotations;
using LightWS.Models;


namespace Utilities
{
  
    public static class Common
    {
        #region Biến
        private static string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY-",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ",
            "/:()?+*&%^$#@!,.\\|:;'\""
        };
        #endregion

        public static string fRemoveSign4VietnameseString(string Str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    Str = Str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return Str;
        }
        public static string GetMD5(string input)
        {
            MD5 x = new MD5CryptoServiceProvider();
            byte[] lam = Encoding.UTF8.GetBytes(input);
            lam = x.ComputeHash(lam);
            StringBuilder s = new StringBuilder();
            foreach (byte b in lam)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
     
        public static string ValueLang(string s)
        {
            try
            {
                return (string)HttpContext.GetGlobalResourceObject("Lang" + "En", s);
            }
            catch
            {
                return "";
            }
        }


        public static string GetValue(string key)
        {
            using (var db = new MarketingOnlineDBEntities())
            {
                var info = db.SystemKeys.FirstOrDefault(s => s.Key.ToLower() == key.ToLower());
                return info == null ? string.Empty : info.Value;
            }
        }
        public static string AppStr(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public static string URLRoot()
        {
            return AppStr("URL");
        }
        public static string ResolveUrl(string path)
        {
            return string.Format("{0}{1}", URLRoot(), path);
        }
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string FormatPrice(double? price, string decimalplaces = "")
        {
            return string.Format("{0:c" + decimalplaces + "}", price ?? 0);
        }
        public static string GetNameStatus(Enum _enum)
        {


            string result = string.Empty;
            var members = _enum.GetType().GetMember(_enum.ToString());
            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var attribute = (DisplayAttribute)attributes[0];
            result = attribute.GetName();
            return result;
        }
        public static string FormatKilo(double? price)
        {
            price = price ?? 0;
            if (price < 1000)
                return FormatPrice(price);


            if (price >= 1000 && price < 1000000)
            {
                return "$" + (price.Value / 1000).ToString("0.#") + "K";
            }
            else if (price >= 1000000 && price < 1000000000)
                return "$" + (price.Value / 1000000).ToString("0.#") + "M";
            else if (price >= 1000000000 && price < 1000000000000)
                return "$" + (price.Value / 1000000000).ToString("0.#") + "B";

            return "$" + price.Value.ToString("#.0");

        }
       
        public static string ClientIP
        {
            get
            {
                string IP = "";
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                if (IP == "")
                {
                    IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                return IP;
            }
        }
        public static int TryParseInt(string val)
        {
            try
            {


                int result = 0;
                int.TryParse(val, out result);
                return result;
            }
            catch
            {

                return 0;
            }
        }
        public static double TryParseDouble(string val)
        {
            try
            {


                double result = 0;
                double.TryParse(val, out result);
                return result;
            }
            catch
            {

                return 0;
            }
        }
        public static float TryParseFloat(string val)
        {
            try
            {


                float result = 0;
                float.TryParse(val, out result);
                return result;
            }
            catch
            {

                return 0;
            }
        }
        public static DateTime? TryParseDateTime(string val)
        {
            try
            {
                DateTime result;
                DateTime.TryParse(val, out result);
                return result;
            }
            catch
            {
                return null;
            }
        }
        public static object TryParseEnum(Type enumType, string value)
        {
            try
            {
                return Enum.Parse(enumType, value);
            }
            catch
            {
                return Enum.Parse(enumType, "USD");
            }
        }
        public static long TryParseLong(string val)
        {
            try
            {


                long result = 0;
                long.TryParse(val, out result);
                return result;
            }
            catch
            {

                return 0;
            }
        }
        public static string GetShortContent(string content)
        {
            var regMask = new Regex(@"(<([^>]+)>)");
            string rep = regMask.Replace(content, "");
            return rep;
        }
        public static string GetCurrentActive(string querystring, string current, string CssClass)
        {
            if (querystring == current)
                return CssClass;
            return string.Empty;
        }
    
    
        public static string GenerateCode(int length, bool onlyNumber = false)
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            const string DigitChars = "0123456789";
            var chars = new char[length];
            var rd = new Random();

            for (int i = 0; i < length; i++)
            {
                if (onlyNumber)
                    chars[i] = DigitChars[rd.Next(0, DigitChars.Length)];
                else
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

    
    
        public static string GetUrl(string actionname)
        {
            return string.Format("{0}{1}", URLRoot(), actionname);
        }
        public static string FormatDate(DateTime date)
        {
            return string.Format("{0:G}", date);
        }
    
    


    
    }
}