using System;
using System.Web.Mvc;
using System.Web.Routing;
using AJ.UtiliTools;
using BC.Models.Post;

namespace BC.Models.Tag.Controllers
{
    public class TagController : Controller
    {
        #region Initialize

        public IPostService PostService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (PostService == null) { PostService = new PostService(); }

            base.Initialize(requestContext);
        }

        #endregion

        #region Index

        public ActionResult Index(String id, int? page)
        {
            if(id.HasValue())
            {
                SearchResult<BC_Post> posts = PostService.GetByTag(true, id, 5, Helper.Page(page));
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
