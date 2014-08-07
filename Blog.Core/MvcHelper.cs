using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Core
{
    public class MvcHelper
    {
        public static UrlHelper GetUrlHelper()
        {
            var httpContext = HttpContext.Current;

            var httpContextBase = new HttpContextWrapper(httpContext);
            var routeData = new RouteData();
            var requestContext = new RequestContext(httpContextBase, routeData);

            return new UrlHelper(requestContext);
        }
    }
}
