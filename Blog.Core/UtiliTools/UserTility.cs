using System;
using System.Web;

namespace AJ.UtiliTools
{
    public class UserTility
    {
        public static String CurrentUserIP
        {
            get
            {
                string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipList))
                {
                    return ipList.Split(',')[0];
                }

                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
        }
    }
}