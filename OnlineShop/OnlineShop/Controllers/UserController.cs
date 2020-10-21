using BotDetect.Web.Mvc;
using Facebook;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// facebook
        /// </summary>
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }



        /// <summary>
        /// đăng nhập với facebook
        /// </summary>
        /// <returns></returns>

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppID"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new User();
                user.Email = email;
                user.UserName = userName;
                user.Status = true;
                user.Name = firstname + " " + middlename + " " + lastname;
                user.CreateDate = DateTime.Now;
                var resultInsert = new UserDao().InssertToFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonContaist.USER_SESSION, userSession);
                }
            }
            return RedirectToAction("index","Home");
        }





        // GET: User

        /// <summary>
        /// đăng nhập phần khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var dao = new UserDao();
                    var relust = dao.Login(model.Username, Encryptor.MD5Hash(model.Password)); // đây là hàm gọi mã hóa md5 nếu không mã hóa thì Login(model.UserName,model.Passworh)
                    if (relust == 1)
                    {
                        var user = dao.GetById(model.Username);
                        var session = new UserLogin();
                        session.UserName = user.UserName;
                        session.UserID = user.ID;
                        Session.Add(CommonContaist.USER_SESSION, session);
                        return RedirectToAction("index","Home");
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
               
            }
            return View(model);
        }


        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>

        public ActionResult Loguot()
        {
            Session[CommonContaist.USER_SESSION] = null;
            return RedirectToAction("Index","Home");
        }


        /// <summary>
        /// đăng kí
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "RegisterCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUsername(register.UserName))
                {
                    ModelState.AddModelError("", "Tài khoảng đả tồn tại");
                }
                else
                {
                    if (dao.CheckEmail(register.Email))
                    {
                        ModelState.AddModelError("", "Email đả tồn tại");
                    }
                    else
                    {
                        var user = new User();
                        user.UserName = register.UserName;
                        user.Password = Encryptor.MD5Hash(register.Password);
                        user.Name = register.Name;
                        user.Email = register.Email;
                        user.Address = register.Address;
                        user.Phone = register.Phone;
                        user.CreateDate = DateTime.Now;
                        user.Status = true;
                        var re =  new UserDao().Insert(user);
                        if(re > 0)
                        {
                            ViewBag.Success = "Đăng kí thành công";
                            register = new RegisterModel(); // restart lại toàn bộ cái register và trả về register
                        }
                        else
                        {
                            ModelState.AddModelError("", "Đăng kí không thành công");
                        }
                    }
                }
            }
            return View(register);
        }
    }
}