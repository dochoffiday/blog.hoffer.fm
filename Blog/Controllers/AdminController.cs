using BC.Authorization.Attributes;
using BC.Data;
using BC.Data.Context;
using BC.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;
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

        [LoggedIn]
        public ActionResult ExportDataToJson()
        {
            var db = new DBDataContext();
            var data = new Hashtable();

            var _pages = new Repository<BC_Page>(db);
            data.Add("Pages", _pages.All().ToList());

            var _categories = new Repository<BC_Category>(db);
            data.Add("Categories", _categories.All().ToList());

            var _tags = new Repository<BC_Tag>(db);
            data.Add("Tags", _tags.All().ToList());

            var _posts = new Repository<BC_Post>(db);
            data.Add("Posts", _posts.All().ToList());

            var _comments = new Repository<BC_Comment>(db);
            data.Add("Comments", _comments.All().ToList());

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            return Content(json, "json");
        }
    }
}