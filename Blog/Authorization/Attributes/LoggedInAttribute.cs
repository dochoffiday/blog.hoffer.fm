using System.Web.Mvc;

namespace BC.Authorization.Attributes
{
    public class LoggedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthorizationBiz.IsAuthenticated)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult(AuthorizationBiz.LoginUrl());
            }
        }
    }
}