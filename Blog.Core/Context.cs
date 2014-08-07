using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BC.Core.Context
{
    public class Current
    {
        public static String Theme { get { return AJ.UtiliTools.UtiliSetting.AppSetting("Theme"); } }

        public static String Domain
        {
            get
            {
                String applicationPath = HttpContext.Current.Request.ApplicationPath == "/" || HttpContext.Current.Request.ApplicationPath == null ? null : HttpContext.Current.Request.ApplicationPath;
                return HttpContext.Current.Request.Url.Host + applicationPath;
            }
        }
    }
}