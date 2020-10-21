using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class AboutController : Controller
    {
        // GET: Admin/About
        public ActionResult Index(string SeachString, int page = 1 , int pagesize = 10)
        {
            var dao = new AboutDao();
            var model = dao.Listpage(SeachString, page, pagesize);
            ViewBag.SeachString = SeachString;
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(About about)
        {
            var dao = new AboutDao();
            var model = dao.Insert(about);
            if (ModelState.IsValid)
            {
                if (model > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "thêm mới không thàng công");
                }
            }
            return View("index");

        }

        public ActionResult Edit(long id)
        {
            var dao = new AboutDao();
            var model = dao.Getupdate(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit (About about)
        {
            var dao = new AboutDao();
            var model = dao.Update(about);
            if (ModelState.IsValid)
            {
                if ( model)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "sửa không thành công");
                }
            }
            return View("Index");
        }

        public ActionResult Delete(long id)
        {
            new AboutDao().Delete(id);
            return RedirectToAction("index");

        }
    }
}