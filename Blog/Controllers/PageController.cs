using BC.Authorization;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BC.Models.Page.Controllers
{
    public class PageController : Controller
    {
        #region Initialize

        public IPageService PageService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (PageService == null) { PageService = new PageService(); }

            base.Initialize(requestContext);
        }

        #endregion

        #region Details

        public ActionResult Details(String slug)
        {
            BC_Page page = AuthorizationBiz.IsAuthenticated ? PageService.GetPageBySlug(null, slug) : PageService.GetPageBySlug(true, slug);

            if (page != null)
            {
                return View(page);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
