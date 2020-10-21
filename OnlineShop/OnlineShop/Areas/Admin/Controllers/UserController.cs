using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string SeachString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.LiskPage(SeachString,page, pageSize);
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var dao = new UserDao().ViewDale(id);
            return View(dao);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            var dao = new UserDao();

           var relust = dao.Upadate(user);
            if (ModelState.IsValid)
            {
                if ( relust)
                {
                    return RedirectToAction("index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật khoong thanh cong");
                }
            }
            return View("index");
        }



        [HttpGet]
        public ActionResult create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            var dao = new UserDao();

            var enctyMD5 = Encryptor.MD5Hash(user.Password);
            user.Password = enctyMD5;

            long id = dao.Insert(user);
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    return RedirectToAction("index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "create khoong thanh cong");
                }
            }
            return View("index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}