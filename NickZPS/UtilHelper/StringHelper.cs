using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NickZPS
{
    public class StringHelper
    {
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static bool IsPictureSuffix(string suffix)
        {
            return (suffix.Equals(".jpg") || suffix.Equals(".jpeg") || suffix.Equals(".png") || suffix.Equals(".gif") || suffix.Equals(".bmp"));
        }

        public static bool IsPicture(string filename)
        {
            if (filename == null || filename.Length <= 0)
                return false;
            string suffix = filename.Substring(filename.LastIndexOf(".")).ToLower();
            return (suffix.Equals(".jpg") || suffix.Equals(".jpeg") || suffix.Equals(".png") || suffix.Equals(".gif") || suffix.Equals(".bmp"));
        }

        public static string FilterSql(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            s = s.Trim().ToLower();
            s = s.Replace("=", "");
            s = s.Replace("'", "");
            s = s.Replace(";", "");
            s = s.Replace(" or ", "");
            s = s.Replace("select", "");
            s = s.Replace("update", "");
            s = s.Replace("insert", "");
            s = s.Replace("delete", "");
            s = s.Replace("declare", "");
            s = s.Replace("exec", "");
            s = s.Replace("drop", "");
            s = s.Replace("create", "");
            s = s.Replace("%", "");
            s = s.Replace("--", "");
            return s;
        }
    }
}