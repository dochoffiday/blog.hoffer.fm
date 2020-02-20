using BC.Authorization.Attributes;
using BC.Models.Post;
using System.Web.Mvc;
using System.Web.Routing;

namespace BC.Models.Comment.Controllers
{
    [LoggedIn]
    public class CommentAdminController : Controller
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

        #region Index

        public ActionResult Index(int postID)
        {
            return View();
        }

        #endregion

        #region MarkAsRead

        public ActionResult MarkAsRead(int postID)
        {
            BC_Post post = PostService.GetPostByID(postID);

            if (post != null)
            {
                return View(post);
            }

            return RedirectToAction("Index", "PostAdmin");
        }

        [HttpPost]
        public RedirectToRouteResult MarkAsRead(int postID, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                foreach (BC_Comment comment in CommentService.Search(postID, false, null, null).Results)
                {
                    comment.IsRead = true;
                }

                CommentService.Save();
            }

            return RedirectToAction("Index", "CommentAdmin");
        }

        #endregion

        #region List

        public ActionResult List(int postID)
        {
            BC_Post post = PostService.GetPostByID(postID);

            if (post == null) return RedirectToAction("Index", "Admin");

            return View(CommentService.Search(postID, null, null, null).Results);
        }

        #endregion

        #region Details

        public ActionResult Details(int id)
        {
            BC_Comment comment = CommentService.GetCommentByID(id);

            comment.IsRead = true;

            CommentService.Save();

            return View(comment);
        }

        #endregion

        #region Edit

        public ActionResult Edit(int id)
        {
            BC_Comment comment = CommentService.GetCommentByID(id);

            comment.IsRead = true;

            CommentService.Save();

            return View(comment);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BC_Comment comment)
        {
            if (ModelState.IsValid)
            {
                CommentService.Update(comment);

                return RedirectToAction("edit", new { id = comment.CommentID });
            }

            return View(comment);
        }

        #endregion

        #region Delete

        public ActionResult Delete(int id)
        {
            BC_Comment comment = CommentService.GetCommentByID(id);

            return View(comment);
        }

        [HttpPost]
        public RedirectToRouteResult Delete(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                BC_Comment comment = CommentService.GetCommentByID(id);

                CommentService.Delete(comment);
            }

            return RedirectToAction("Index", "CommentAdmin");
        }

        #endregion
    }
}
