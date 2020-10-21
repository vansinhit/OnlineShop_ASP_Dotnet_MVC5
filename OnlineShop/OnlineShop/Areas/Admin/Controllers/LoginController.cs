using Model.Dao;

using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var relust = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password)); // đây là hàm gọi mã hóa md5 nếu không mã hóa thì Login(model.UserName,model.Passworh)
                if (relust == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var session = new UserLogin();
                    session.UserName = user.UserName;
                    session.UserID = user.ID;
                    Session.Add(CommonContaist.USER_SESSION, session);
                    return RedirectToAction("index", "Home");
                }
                else if (relust == 0)
                {
                    ModelState.AddModelError("", "Tài khoảnh không tồn tại");
                }
                else if (relust == -1)
                {
                    ModelState.AddModelError("", "Tài khoảnh đang bị khóa");
                }
                else if (relust == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không chính xác");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }

            }
            return View(model);
        }

      
        public ActionResult Loguot()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}