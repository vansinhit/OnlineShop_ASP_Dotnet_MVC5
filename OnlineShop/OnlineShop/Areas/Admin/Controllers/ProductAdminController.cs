using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductAdminController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string SeachString, int page =1 , int pagesize = 2)
        {
            var dao = new ProductAdminDao();
            var model = dao.ListPage(SeachString, page, pagesize);
            ViewBag.SeachString = SeachString;
                return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            var dao = new ProductAdminDao();
            var model = dao.Insert(product);
            if (ModelState.IsValid)
            {
                if(model > 0)
                {
                    SetAlert("thêm sản phẩm  thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "thêm không thành công");
                }
            }
            return View("index");
        }
        public ActionResult Edit(long id)
        {
            var dao = new ProductAdminDao().GetUpdate(id);
            return View(dao);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            var dao = new ProductAdminDao();
            var model = dao.Update(product);
            if (ModelState.IsValid)
            {
                if (model)
                {
                    SetAlert("Sửa sản phẩm thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "sửa không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]

        public ActionResult Delete(long id)
        {
            new ProductAdminDao().Delete(id);
            return RedirectToAction("index");
        }
    }
}