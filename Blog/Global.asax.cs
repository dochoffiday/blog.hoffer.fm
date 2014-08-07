using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Feed", // Route name
                "home/feed", // URL with parameters
                new { controller = "Home", action = "Feed" } // Parameter defaults
            );

            routes.MapRoute(
                "Sitemap", // Route name
                "home/sitemap", // URL with parameters
                new { controller = "Home", action = "Sitemap" } // Parameter defaults
            );

            routes.MapRoute(
                "admin",
                "admin",
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
                new string[] { "BC.Controllers" }
            );

            routes.MapRoute(
                "Category_Admin",
                "admin/category/{action}/{id}",
                new { controller = "categoryadmin", action = "index", id = UrlParameter.Optional },
                new string[] { "BC.Models.Category.Controllers" }
            );

            routes.MapRoute(
                "Comment_Admin", // Route name
                "admin/post/{postID}/comment/{action}/{id}", // URL with parameters
                new { controller = "CommentAdmin", action = "Index", id = UrlParameter.Optional },
                new string[] { "BC.Models.Comment.Controllers" }
            );

            routes.MapRoute(
                "Page_Admin",
                "admin/page/{action}/{id}",
                new { controller = "pageadmin", action = "index", id = UrlParameter.Optional },
                new string[] { "BC.Models.Page.Controllers" }
            );

            routes.MapRoute(
                "Post_Admin",
                "admin/post/{action}/{id}",
                new { controller = "postadmin", action = "index", id = UrlParameter.Optional },
                new string[] { "BC.Models.Post.Controllers" }
            );

            routes.MapRoute(
                "Login",
                "account/{action}",
                new { controller = "account", action = "index", id = UrlParameter.Optional },
                new string[] { "BC.Models.Account.Controllers" }
            );

            routes.MapRoute(
                "Category",
                "category/{id}",
                new { controller = "category", action = "index", id = UrlParameter.Optional },
                new string[] { "BC.Models.Category.Controllers" }
            );

            routes.MapRoute(
                "Comment",
                "comment/{action}/{id}",
                new { controller = "comment", action = "index", id = UrlParameter.Optional },
                new string[] { "BC.Models.Comment.Controllers" }
            );

            routes.MapRoute(
                "Tag",
                "tag/{id}",
                new { controller = "tag", action = "index", id = UrlParameter.Optional },
                new string[] { "BC.Models.Tag.Controllers" }
            );

            routes.MapRoute(
                "Post",
                "{category}/{slug}",
                new { controller = "post", action = "details" },
                new string[] { "BC.Models.Post.Controllers" }
            );

            routes.MapRoute(
                "Page",
                "{slug}",
                new { controller = "page", action = "details" },
                new string[] { "BC.Models.Page.Controllers" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "home", action = "index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //if (AJ.UtiliTools.UtiliSetting.AppSetting("DebugRoutes") == "True")
            //{
            //    RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            //}
        }
    }
}