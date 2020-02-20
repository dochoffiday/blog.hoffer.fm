using AJ.UtiliTools;
using BC.Authorization.Attributes;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace BC.Models.Page.Controllers
{
    [LoggedIn]
    public class PageAdminController : Controller
    {
        #region Initialize

        public IPageService PageService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (PageService == null) { PageService = new PageService(); }

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
            IQueryable<BC_Page> pages = PageService.Search(null, name, null, null).Results;

            return View(pages);
        }

        #endregion

        #region Details

        public ActionResult Details(int id)
        {
            BC_Page page = PageService.GetPageByID(id);

            return View(page);
        }

        #endregion

        #region Create

        public ActionResult Create()
        {
            return View(PageService.GetInstance());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BC_Page page)
        {
            if (ModelState.IsValid)
            {
                page.Title = Helper.UrlDecode(page.Title);
                page.Slug = Helper.UrlDecode(page.Slug);

                PageService.Insert(page);

                return RedirectToAction("Index", "PageAdmin");
            }

            return View(page);
        }

        #endregion

        #region Edit

        public ActionResult Edit(int id)
        {
            BC_Page page = PageService.GetPageByID(id);

            return View(page);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BC_Page page)
        {
            if (ModelState.IsValid)
            {
                page.Title = Helper.UrlDecode(page.Title);
                page.Slug = Helper.UrlDecode(page.Slug);

                PageService.Update(page);

                return RedirectToAction("edit", new { id = page.PageID });
            }

            return View(page);
        }

        #endregion

        #region Delete

        public ActionResult Delete(int id)
        {
            BC_Page page = PageService.GetPageByID(id);

            return View(page);
        }

        [HttpPost]
        public RedirectToRouteResult Delete(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                BC_Page page = PageService.GetPageByID(id);

                PageService.Delete(page);
            }

            return RedirectToAction("Index", "PageAdmin");
        }

        #endregion
    }
}
