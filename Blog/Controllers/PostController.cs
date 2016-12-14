using AJ.UtiliTools;
using BC.Authorization;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BC.Models.Post.Controllers
{
    public class PostController : Controller
    {
        #region Initialize

        public IPostService PostService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (PostService == null) { PostService = new PostService(); }

            base.Initialize(requestContext);
        }

        #endregion

        #region List

        public ActionResult List(SearchResult<BC_Post> result)
        {
            return View(result);
        }

        #endregion

        #region Search

        public ActionResult Search(int? page)
        {
            return View(PostService.Search(true, null, null, 5, Helper.Page(page)));
        }

        #endregion

        #region Preview

        public ActionResult Preview(BC_Post post)
        {
            if (post != null)
            {
                return View(post);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Info

        public ActionResult Info(BC_Post post)
        {
            if (post != null)
            {
                return View(post);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Paging

        public ActionResult Paging(SearchResult<BC_Post> result)
        {
            return View(result);
        }

        #endregion

        #region Details

        public ActionResult Details(String category, String slug)
        {
            BC_Post post = AuthorizationBiz.IsAuthenticated ? PostService.GetPostBySlug(null, category, slug) : PostService.GetPostBySlug(true, category, slug);

            if (post != null)
            {
                return View(post);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
