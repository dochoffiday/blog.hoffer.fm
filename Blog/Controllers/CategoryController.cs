using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BC.Models.Post;
using AJ.UtiliTools;

namespace BC.Models.Category.Controllers
{
    public class CategoryController : Controller
    {
        #region Initialize

        public ICategoryService CategoryService { get; set; }
        public IPostService PostService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (CategoryService == null) { CategoryService = new CategoryService(); }
            if (PostService == null) { PostService = new PostService(); }

            base.Initialize(requestContext);
        }

        #endregion

        #region Index

        public ActionResult Index(String id, int? page)
        {
            BC_Category category = CategoryService.GetCategoryByName(id);

            if (category != null)
            {
                SearchResult<BC_Post> posts = PostService.Search(true, category.CategoryID, null, 5, Helper.Page(page));
                return View(posts);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Paging

        public ActionResult Paging(String id, SearchResult<BC_Post> result)
        {
            ViewBag.ID = id;

            return View(result);
        }

        #endregion
    }
}
