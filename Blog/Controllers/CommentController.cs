using BC.Authorization;
using BC.Models.Post;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace BC.Models.Comment.Controllers
{
    public class CommentController : Controller
    {
        #region Initialize

        public ICommentService CommentService { get; set; }
        public IPostService PostService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (CommentService == null) { CommentService = new CommentService(); }
            if (PostService == null) { PostService = new PostService(); }

            base.Initialize(requestContext);
        }

        #endregion

        #region Details

        public ActionResult Details(BC_Comment comment)
        {
            return View(comment);
        }

        #endregion

        #region List

        public ActionResult List(BC_Post post)
        {
            ViewBag.PostID = post.PostID;
            ViewBag.IsCommentsOpen = post.IsCommentsOpen;

            return View(CommentService.Search(post.PostID, null, null, null));
        }

        #endregion

        #region Create

        public ActionResult Create(int postID)
        {
            ViewBag.PostID = postID;

            return View();
        }

        [HttpPost]
        public ActionResult Create(int postID, BC_Comment comment)
        {
            if (String.IsNullOrWhiteSpace(Request["body"]))
            {
                BC_Post post = PostService.GetPostByID(postID);

                if (post != null)
                {
                    if (ModelState.IsValid)
                    {
                        comment.Created = DateTime.UtcNow;
                        comment.Modified = DateTime.UtcNow;

                        if (AuthorizationBiz.IsAuthenticated)
                        {
                            comment.IsUser = true;
                        }

                        comment.PostID = post.PostID;

                        CommentService.Insert(comment);

                        return View("Details", comment);
                    }
                }
            }

            return Content("FAIL: " + string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(p => p.ErrorMessage)));
        }

        #endregion
    }
}
