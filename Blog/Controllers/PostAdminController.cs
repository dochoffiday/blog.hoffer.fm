using AJ.UtiliTools;
using BC.Authorization.Attributes;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace BC.Models.Post.Controllers
{
    [LoggedIn]
    public class PostAdminController : Controller
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

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region List

        public ActionResult List(String name, Nullable<bool> enabled)
        {
            IQueryable<BC_Post> posts = PostService.Search(null, null, name, null, null).Results;

            return View(posts);
        }

        #endregion

        #region Details

        public ActionResult Details(int id)
        {
            BC_Post post = PostService.GetPostByID(id);

            return View(post);
        }

        #endregion

        #region Create

        public ActionResult Create()
        {
            BC_Post post = PostService.GetInstance();

            post.IsCommentsOpen = true;
            post.IsCommentsVisible = true;

            return View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BC_Post post, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                post.Title = Helper.UrlDecode(post.Title);
                post.Slug = Helper.UrlDecode(post.Slug);

                PostService.Insert(post, BC.Core.Helper.CleanTags(collection["Tags"]));

                return RedirectToAction("Index", "PostAdmin");
            }

            return View(post);
        }

        #endregion

        #region Edit

        public ActionResult Edit(int id)
        {
            BC_Post post = PostService.GetPostByID(id);

            return View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BC_Post post, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                post.Title = Helper.UrlDecode(post.Title);
                post.Slug = Helper.UrlDecode(post.Slug);

                PostService.Update(post, BC.Core.Helper.CleanTags(collection["Tags"]));

                return RedirectToAction("Index", "PostAdmin");
            }

            return View(post);
        }

        #endregion

        #region Delete

        public ActionResult Delete(int id)
        {
            BC_Post post = PostService.GetPostByID(id);

            return View(post);
        }

        [HttpPost]
        public RedirectToRouteResult Delete(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                BC_Post post = PostService.GetPostByID(id);

                PostService.Delete(post);
            }

            return RedirectToAction("Index", "PostAdmin");
        }

        #endregion
    }
}
