using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductCategory(int Caid)
        {
            var dao = new ProductCategoryDao().ViewProductCategoryDetail(Caid);
            ViewBag.Category = dao;
            var model = new ProductDao().ListByCategoryId(Caid);
            return View(model);
        }
        public ActionResult ProductDetail(int idproduct)
        {
            var dao = new ProductDao().GetProductDetail(idproduct);
            ViewBag.productDetail = new ProductDao().ListProductHot(4);
            ViewBag.Category = new ProductCategoryDao().ViewProductCategoryDetail(dao.CategoryID.Value);
            return View(dao);
        }
    }
}