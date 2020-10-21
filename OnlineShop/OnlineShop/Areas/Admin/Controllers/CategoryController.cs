using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string SeachString,int page = 1, int pageSize=2)
        {
            var dao = new CategoryDao();
            var model = dao.ListPage(SeachString,page,pageSize);
            ViewBag.SeachString = SeachString;
            return View(model);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            var create = new CategoryDao();
            var id = create.Insert(category);
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", StaticResouce.Resources.InsertCategoryFail);
                }
            }

            return View("index");


        }

        public ActionResult Edit(int id)
        {
            var dao = new CategoryDao().ViewUpdate(id);
            return View(dao);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            var create = new CategoryDao();
            var id = create.Update(category);
            if (ModelState.IsValid)
            {
                if (id)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", StaticResouce.Resources.UpdateCategoryFail);
                }
            }

            return View("index");


        }
        [HttpDelete]

        public ActionResult Delete(int id)
        {
            var dao = new CategoryDao();
            dao.Deletecategory(id);
            return RedirectToAction("Index");
        }

    }
}