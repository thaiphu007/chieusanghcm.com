
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;



namespace Utilities.Extensions
{
    public class MyCustomDateProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;

            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!(arg is DateTime)) throw new NotSupportedException();

            var dt = (DateTime)arg;

            string suffix;

            if (new[] { 11, 12, 13 }.Contains(dt.Day))
            {
                suffix = "th";
            }
            else if (dt.Day % 10 == 1)
            {
                suffix = "st";
            }
            else if (dt.Day % 10 == 2)
            {
                suffix = "nd";
            }
            else if (dt.Day % 10 == 3)
            {
                suffix = "rd";
            }
            else
            {
                suffix = "th";
            }

            return string.Format("{0:MMMM} {1}{2}, {0:yyyy}", arg, dt.Day, suffix);
        }
    }
    public static class ObjectExtensions
    {
        public static string ToJson(this Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// Phương thức thay thế parameter truyền theo thứ tự.
        /// Ex: {Param1} {Param2} {Param3} ....
        /// Objects[]: [String Repace Param1,StringRepace Param2,StringRepace Param3,.... ]
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        /// <returns></returns>
         public static string FormatString(this string text, params object[] args)
        {
            if (args == null) return text;
            var regMask = new Regex(@"\{(?<PropertyName>\w*)\}");
            Match match = regMask.Match(text);
            int index = 0;
            var listmatch = new List<string>();
            while (match.Success)
            {
                string propertyName = match.Groups["PropertyName"].Value;
                
                match = match.NextMatch();
                if (!listmatch.Contains(propertyName))
                {
                    listmatch.Add(propertyName);
                    if (args.Length - 1 >= index)
                        text = text.Replace(string.Format("{{{0}}}", propertyName), args[index].ToString());
                    index++;
                }

            }
            return text;
        }
         public static HtmlString FormatString(this HtmlString text, params object[] args)
         {
             if (args == null) return text;
             var result = text.ToString();
             var regMask = new Regex(@"\{(?<PropertyName>\w*)\}");
             Match match = regMask.Match(result);
             int index = 0;
             var listmatch = new List<string>();
             while (match.Success)
             {
                 string propertyName = match.Groups["PropertyName"].Value;

                 match = match.NextMatch();
                 if (!listmatch.Contains(propertyName))
                 {
                     listmatch.Add(propertyName);
                     if (args.Length - 1 >= index)
                         result = result.Replace(string.Format("{{{0}}}", propertyName), args[index].ToString());
                     index++;
                 }

             }
             return new HtmlString(result);
         }
         public static string FormatDateTime(this DateTime text)
         {
             var formattedDate = string.Format(new MyCustomDateProvider(), "{0}", text);
             return formattedDate;
         }
         public static string RemoveSpecialChars(this string text)
        {
            var regMask = new Regex(@"[^0-9a-zA-Z ]+");
            string result = regMask.Replace(text, "");
            return result;
        }
  
        public static string RemoveHtmlTags(this string text)
        {
            var regMask = new Regex(@"(<([^>]+)>)");
            string result = regMask.Replace(text, "");
            return result;
        }
        public static string RemoveScriptsTags(this string text)
        {
            var regMask = new Regex(@"<scripts.*>.*<\/scripts>", RegexOptions.IgnoreCase);
            string result = regMask.Replace(text, "");
            return result;
        }
        public static string RemoveIframeTags(this string text)
        {
            var regMask = new Regex(@"(<iframe.*>.*<\/iframe>)|(<iframe.*/>)",RegexOptions.IgnoreCase);
            string result = regMask.Replace(text, "");
            return result;
        }
        public static string RemoveHtmlTags(this string text,params string[] htmltags)
        {
            string reg=@"<\s*{0}[^>]*>|<\s*/\s*{0}>";
            string result = text;
            foreach (var item in htmltags)
            {
                var regMask = new Regex(string.Format(reg,item), RegexOptions.IgnoreCase);
                result = regMask.Replace(result, "");    
            }
            
            return result;
        }
        
        public static string Slice(this string text,char separate,int NumberParaph,int limit=250 )
        {   
            
            string[] arrStr = text.Split(separate);

            string result = string.Empty;
            if (NumberParaph > 0 && NumberParaph <= arrStr.Length - 1)
            {
                for (int i = 0; i < NumberParaph; i++)
                {
                   if(string.IsNullOrEmpty(result))
                    result= arrStr[i]+separate;    
                   else
                       result = result + arrStr[i]+separate;    
                }
                
            }else 
                result = text;
            if(result.Length>limit)
            {
                var indexLast = result.LastIndexOf(' ');
                result = result.Substring(0, indexLast);
                
            }
            return result.Length < 500 ? result : result.Substring(0,400)+" ...";
        }

        public static string ToRelativeDate(this DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return string.Format("{0} seconds ago", timeSpan.Seconds);

            if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes > 1 ? String.Format("about {0} minutes ago", timeSpan.Minutes) : "about a minute ago";

            if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours > 1 ? String.Format("about {0} hours ago", timeSpan.Hours) : "about an hour ago";

            if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days > 1 ? String.Format("about {0} days ago", timeSpan.Days) : "yesterday";

            if (timeSpan <= TimeSpan.FromDays(365))
                return timeSpan.Days > 30 ? String.Format("about {0} months ago", timeSpan.Days / 30) : "about a month ago";

            return timeSpan.Days > 365 ? String.Format("about {0} years ago", timeSpan.Days / 365) : "about a year ago";
        }
        public static string DateString(this DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return string.Format("{0} seconds", timeSpan.Seconds);

            else if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes >= 1 ? String.Format("{0} minutes", timeSpan.Minutes) : "a minute";

            else if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours >= 1 ? String.Format("{0} hours", timeSpan.Hours) : "an hour";

            else if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days >= 1 ? String.Format("{0} days", timeSpan.Days) : "yesterday";

            else if (timeSpan <= TimeSpan.FromDays(365))
                return timeSpan.Days >= 30 ? String.Format("{0} months", timeSpan.Days / 30) : "a month";

            return timeSpan.Days >= 365 ? String.Format("{0} years", timeSpan.Days / 365) : "a year";
        }
        public static string FormatDate(this DateTime dateTime)
        {
            return Common.FormatDate(dateTime);
        }
       
    }
    
}