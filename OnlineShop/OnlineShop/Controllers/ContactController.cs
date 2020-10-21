using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var dao = new ContactDao();
            var model = dao.GetActiveContact();
            return View(model);
        }
        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var feedback = new FeedBack();
            feedback.Name = name;
            feedback.Phone = mobile;
            feedback.Address = address;
            feedback.CreateDate = DateTime.Now;
            feedback.Email = email;
            feedback.Content = content;
            var id = new FeedBackDao().Insert(feedback);
            if (id > 0)
            {
                //gửi mail cho quản trị
               
                string sendmail = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/Tempelet/feeback.html"));

                sendmail = sendmail.Replace("{{Name}}", name);
                sendmail = sendmail.Replace("{{Phone}}", mobile);
                sendmail = sendmail.Replace("{{Email}}", email);
                sendmail = sendmail.Replace("{{Address}}", address);
                sendmail = sendmail.Replace("{{Content}}", content);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new MailHelper().SendMail(toEmail, "phản hổi từ khách hàng", sendmail);

                return Json(new
                {
                    status = false
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }

           

           

          

        }
    }
}
