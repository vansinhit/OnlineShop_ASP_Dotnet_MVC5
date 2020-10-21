using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string SeachString , int page =1 , int pagesize=10)
        {
            var dao = new ContentDao();
            var model=  dao.ListAll(SeachString, page, pagesize);
            return View(model);
        }
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Content content)
        {
            var dao = new ContentDao();
            var model = dao.Insert(content);
            if (ModelState.IsValid)
            {
                if(model > 0)
                {
                    SetAlert("thêm user thành công", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("","create không thàn công");
                }
            }
            SetViewBag();
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            SetViewBag();
            var dao = new ContentDao().ViewUpdate(id);
            return View(dao);
        }
        [HttpPost]
        public ActionResult Edit(Content content)
        {
            var dao = new ContentDao();
            var model = dao.Update(content);
            if (ModelState.IsValid)
            {
                if (model)
                {
                    SetAlert("Sửa user thành công", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa không thành công");
                }
            }
            SetViewBag();
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContentDao().Delete(id);
            return RedirectToAction("Index");
        }
        public void SetViewBag(long? selected = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.AllList(), "ID", "Name", selected);
        }


    }
}