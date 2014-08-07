using BC.Authorization.Attributes;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BC.Models.Category.Controllers
{
    [LoggedIn]
    public class CategoryAdminController : Controller
    {
        #region Initialize

        public ICategoryService CategoryService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (CategoryService == null) { CategoryService = new CategoryService(); }

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
            IEnumerable<BC_Category> categorys = CategoryService.Search(name).Results;

            return View(categorys);
        }

        #endregion

        #region Details

        public ActionResult Details(int id)
        {
            BC_Category category = CategoryService.GetCategoryByID(id);

            return View(category);
        }

        #endregion

        #region Create

        public ActionResult Create()
        {
            return View(CategoryService.GetInstance());
        }

        [HttpPost]
        public ActionResult Create(BC_Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryService.Insert(category);

                return RedirectToAction("Index", "CategoryAdmin");
            }

            return View(category);
        }

        #endregion

        #region Edit

        public ActionResult Edit(int id)
        {
            BC_Category category = CategoryService.GetCategoryByID(id);

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(BC_Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryService.Update(category);

                return RedirectToAction("Index", "CategoryAdmin");
            }

            return View(category);
        }

        #endregion

        #region Delete

        public ActionResult Delete(int id)
        {
            BC_Category category = CategoryService.GetCategoryByID(id);

            return View(category);
        }

        [HttpPost]
        public RedirectToRouteResult Delete(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                BC_Category category = CategoryService.GetCategoryByID(id);

                CategoryService.Delete(category);
            }

            return RedirectToAction("Index", "CategoryAdmin");
        }

        #endregion
    }
}
