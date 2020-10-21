using Model.Dao;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            var dao = new MenuDao();
            var model = dao.ListGrop(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult TopMenu()
        {
            var dao = new MenuDao();
            var model = dao.ListGrop(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var dao = new ProductCategoryDao();
            var model = dao.listAll();
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult Slide()
        {
            var dao = new SlideDao();
            var model = dao.listAll();
            return PartialView(model);
        } 
        [ChildActionOnly]
        public PartialViewResult ProductNew()
        {
            var dao = new ProductDao();
            var model = dao.ListProductNew(4);
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult ProductHot()
        {
            var dao = new ProductDao();
            var model = dao.ListProductHot(4);
            return PartialView(model);
        } 
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonContaist.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}