using BC.Authorization.Attributes;
using System.Web.Mvc;

namespace BC.Controllers
{
    public class AdminController : Controller
    {
        [LoggedIn]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the BlogCore Admin";

            return View();
        }
    }
}