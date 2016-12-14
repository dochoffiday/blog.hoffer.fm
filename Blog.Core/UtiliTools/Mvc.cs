using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace AJ.UtiliTools
{
    public class Mvc
    {
        public static SelectListItem SelectListItem(String text, String value, bool selected)
        {
            SelectListItem item = new SelectListItem();

            item.Text = text;
            item.Value = value;
            item.Selected = selected;

            return item;
        }

        public static Uri FullUrl(String action, String controller)
        {
            UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            String url = AJ.UtiliTools.Helper.ToAbsoluteUrl(Url.Action(action, controller));

            return new Uri(url);
        }

        public static Uri FullUrl(String action, String controller, object routeValues)
        {
            UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            String url = AJ.UtiliTools.Helper.ToAbsoluteUrl(Url.Action(action, controller, routeValues));

            return new Uri(url);
        }
    }

    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute() : base("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$") {} 
    }
}
