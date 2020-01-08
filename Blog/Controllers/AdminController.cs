using BC.Authorization.Attributes;
using BC.Data;
using BC.Data.Context;
using BC.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
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
            var _categories = new Repository<BC_Category>(db).All().ToList();
            var _tags = new Repository<BC_Tag>(db).All().ToList();
            var _comments = new Repository<BC_Comment>(db).All().ToList();
            var data = new Hashtable();

            var _pages = new Repository<BC_Page>(db);
            data.Add("Pages", _pages.All().ToList());

            var _posts = new Repository<BC_Post>(db);
            var posts = new List<dynamic>();
            foreach(var post in _posts.All().ToList())
            {
                var dynamicPost = ToDynamic(post);
                dynamicPost.Category = _categories.FirstOrDefault(x => x.CategoryID == post.CategoryID);
                dynamicPost.Tags = _tags.Where(x => x.PostID == post.PostID).OrderBy(x => x.Name).Select(x => x.Name).ToList();
                dynamicPost.Comments = _comments.Where(x => x.PostID == post.PostID).OrderBy(x => x.Created).ToList();

                posts.Add(dynamicPost);
            }

            data.Add("Posts", posts);

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            return Content(json, "json");
        }

        private dynamic ToDynamic(object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }
    }
}