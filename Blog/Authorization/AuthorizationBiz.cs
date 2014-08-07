using AppHarbor.Web.Security;
using Blog.Core;
using System;
using System.Web;

namespace BC.Authorization
{
    public class AuthorizationBiz
    {
        public static void Logout()
        {
            IAuthenticator authenticator = new CookieAuthenticator();

            authenticator.SignOut();
        }

        public static void Login(string username)
        {
            IAuthenticator authenticator = new CookieAuthenticator();

            authenticator.SetCookie(username, true);
        }

        public static String CurrentUsername
        {
            get
            {
                try
                {
                    return HttpContext.Current.User.Identity.Name;
                }
                catch
                {
                    return "";
                }
            }
        }

        public static bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrWhiteSpace(CurrentUsername);
            }
        }

        public static string LoginUrl()
        {
            var urlHelper = MvcHelper.GetUrlHelper();

            return urlHelper.Action("Login", "Account", new { ReturnUrl = HttpContext.Current.Request.Url.AbsoluteUri });
        }
    }
}