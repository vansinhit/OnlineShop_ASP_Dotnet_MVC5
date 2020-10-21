using Common;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
       
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonContaist.CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }


        //Dellte All cart

            public JsonResult DeleteAll()
        {
            Session[CommonContaist.CartSession] = null;
            return Json(new
            {
                status = true
            });

        }

        /// <summary>
        /// xóa từng sản phẩm
        /// </summary>
        /// <returns></returns>
        public JsonResult Delete(long id)
        {
            var sesionCart = (List<CartItem>)Session[CommonContaist.CartSession];
            sesionCart.RemoveAll(x => x.Product.ID == id);
            Session[CommonContaist.CartSession] = sesionCart;
            return Json(new
            {
                status = true
            });

        }

        // Update cart
        public JsonResult Update (string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sesionCart = (List<CartItem>)Session[CommonContaist.CartSession];

            foreach( var item in sesionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CommonContaist.CartSession] = sesionCart;
            return Json(new
            {
                status = true
            });
        }
        //Add cart
        public ActionResult AddCart(int productID, int quantityN) // đây là 2 đối tượn được nhập vào
        {
            var dao = new ProductDao().GetProductDetail(productID);
            var Cart = Session[CommonContaist.CartSession];
            if (Cart != null) // nếu đối tượn đó đả có trong giỏ hàng rồi

            {// ở đây nó đả có rồi thì chắc chắn nó sẻ có 1 cái list Sesion rồi

                var list = (List<CartItem>)Cart;//  lấy ra 1 cái list của cart Session
                if(list.Exists(x=>x.Product.ID == productID)) // nếu cái ID product đả tồn tại trong cái list nớ rồi
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productID) // nếu cái id của product == cái id truyền vào 
                        {
                            item.Quantity += quantityN;
                        }
                    }
                }else // nếu cái IDProduct không tồn tại thì  ta lại Add mới 
                {
                    //add mới 1 đối tương 
                    var item = new CartItem();
                    item.Product = dao;
                    item.Quantity = quantityN;
                    list.Add(item); // ta adđ nó vài cái item ở trên
                }
                // gán vào Sesion
                Session[CommonContaist.CartSession] = list;
             
            }
            else // đối tượng đó chưa được có trong giỏ hàng (còn không ta phải add mới đối tượng đó)
            {
                //tạo mới một đối tượng cart 
                var item = new CartItem();
                item.Product = dao;
                item.Quantity = quantityN;
                var list = new List<CartItem>();
                list.Add(item);
                // gán vào cái Sesion ở trên Cart
                Session[CommonContaist.CartSession] = list;

            }
            return RedirectToAction("Index");
        }

        public ActionResult Payment()
        {
            var cart = Session[CommonContaist.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        /// <summary>
        /// post dử liệu từ Payment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Payment(string shipname, string mobile, string address, string email)
        {
            var order = new OrDer();
            order.CreateDate = DateTime.Now;
            order.ShipName = shipname;
            order.ShipMobile = mobile;
            order.ShipAddress = address;
            order.ShipEmail = email;
            

            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CommonContaist.CartSession];
                var detaildao = new OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;

                    detaildao.Insert(orderDetail);

                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity); // lấy cái tổng tiền để gán xún Email

                    // các phương thức gửi mail

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/Tempelet/Neworder.html"));

                    content = content.Replace("{{CustomerName}}", shipname);
                    content = content.Replace("{{Phone}}", mobile);
                    content = content.Replace("{{Email}}", email);
                    content = content.Replace("{{Address}}", address);
                    content = content.Replace("{{Total}}", total.ToString("N0"));
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                    new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content); //mail gửi cho khách hàng
                    new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);// mail gửi cho quản trị
                }
            }catch(Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}