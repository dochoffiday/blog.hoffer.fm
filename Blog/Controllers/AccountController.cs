using AJ.UtiliTools;
using BC.Authorization;
using BC.Models.ViewModels;
using System.Web.Mvc;
using System.Web.Routing;

namespace BC.Models.Account.Controllers
{
    public class AccountController : Controller
    {
        public IUserService UserService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (UserService == null) { UserService = new UserService(); }

            base.Initialize(requestContext);
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string error = "";
                if(!UserService.Login(model.Username, model.Password, ref error))
                {
                    ModelState.AddModelError("LoginError", error);

                    return View(model);
                }

                AuthorizationBiz.Login(model.Username);

                var returnUrl = Helper.GetQS<string>("ReturnUrl");
                return Redirect(!string.IsNullOrWhiteSpace(returnUrl) ? returnUrl : Url.Action("Index", "Home"));
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            AuthorizationBiz.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}
