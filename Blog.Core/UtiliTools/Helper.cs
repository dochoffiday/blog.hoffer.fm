using System;
using System.Web;

/// <summary>
/// Summary description for Helper
/// </summary>
namespace AJ.UtiliTools
{
    public class Helper
    {
        public static void ConditionalRedirect(String path, bool condition)
        {
            if (condition)
            {
                HttpResponse response = HttpContext.Current.Response;

                response.Redirect(path);
            }
        }

        public static T IIF<T>(bool condition, T a, T b)
        {
            T x = (T)default(T);
            if (condition)
                x = a;
            else
                x = b;

            return x;
        }

        public static T GetDefault<T>()
        {
            return (T)default(T);
        }

        public static DateTime? ParseDateTime(object value)
        {
            DateTime returnVal;

            if (DateTime.TryParse(value.ToString(), out returnVal))
            {
                return returnVal;
            }

            return null;
        }

        public static int? ParseInt(object value)
        {
            int returnVal;

            if (Int32.TryParse(value.ToString(), out returnVal))
            {
                return returnVal;
            }

            return null;
        }

        public static bool? ParseBool(object value)
        {
            bool returnVal;

            if (Boolean.TryParse(value.ToString(), out returnVal))
            {
                return returnVal;
            }

            return null;
        }

        public static T Parse<T>(object value)
        {
            object tmpS = new object();
            if (value != null)
            {
                try
                {
                    tmpS = value;
                }
                catch
                {
                    tmpS = "";
                }
            }
            try
            {
                return (T)System.Convert.ChangeType(tmpS, typeof(T));
            }
            catch
            {
                return (T)default(T);
            }
        }

        public static T Parse<T>(object value, T defaultValue)
        {
            object tmpS = new object();
            if (value != null)
            {
                try
                {
                    tmpS = value;
                }
                catch
                {
                    tmpS = "";
                }
            }
            try
            {
                return (T)System.Convert.ChangeType(tmpS, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T GetQS<T>(String paramName)
        {
            String tmpS = String.Empty;
            if (HttpContext.Current.Request.QueryString[paramName] != null)
            {
                try
                {
                    tmpS = HttpContext.Current.Request.QueryString[paramName].ToString();
                }
                catch
                {
                    tmpS = "";
                }
            }
            try
            {
                return (T)Convert.ChangeType(tmpS, typeof(T));
            }
            catch
            {
                return (T)default(T);
            }
        }

        public static T GetQS<T>(String paramName, T defaultValue)
        {
            String tmpS = String.Empty;
            if (HttpContext.Current.Request.QueryString[paramName] != null)
            {
                try
                {
                    tmpS = HttpContext.Current.Request.QueryString[paramName].ToString();
                }
                catch
                {
                    tmpS = "";
                }
            }
            try
            {
                return (T)Convert.ChangeType(tmpS, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static void UrlDecode(ref String s)
        {
            s = System.Web.HttpUtility.UrlDecode(s);
        }

        public static void UrlEncode(ref String s)
        {
            s = System.Web.HttpUtility.UrlEncode(s);
        }

        public static String UrlDecode(String s)
        {
            return System.Web.HttpUtility.UrlDecode(s);
        }

        public static String UrlEncode(String s)
        {
            return System.Web.HttpUtility.UrlEncode(s);
        }

        public static void Escape(ref String s)
        {
            if (!s.IsNullOrEmpty())
                s = s.Replace("'", "''"); ;
        }

        public static String Escape(String s)
        {
            if (!s.IsNullOrEmpty())
                return s.Replace("'", "''");
            else
                return null;
        }

        public static int Page(int? page)
        {
            if (!page.HasValue)
                return 1;

            if (page.Value <= 1)
                return 1;

            return page.Value;
        }

        public static string ToAbsoluteUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null)
                return relativeUrl;

            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Insert(0, "~");
            if (!relativeUrl.StartsWith("~/"))
                relativeUrl = relativeUrl.Insert(0, "~/");

            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

            return string.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }
    }
}