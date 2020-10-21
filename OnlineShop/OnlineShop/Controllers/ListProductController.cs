using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ListProductController : Controller
    {
        // GET: ListProduct
        public ActionResult Index(string SeachString, int page = 1, int pagesize = 2)
        {
            var dao = new MenuProductDao();
            var model = dao.ListName(SeachString, page, pagesize);
            ViewBag.SeachString = SeachString;
            return View(model);
        }
    }
}