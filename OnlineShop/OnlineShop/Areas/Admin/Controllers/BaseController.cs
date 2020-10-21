using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Đa ngôn ngữ
        /// </summary>
        /// <param name="requestContext"></param>
        //initilizing culture on controller initializationpa
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[CommonContaist.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[CommonContaist.CurrentCulture].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[CommonContaist.CurrentCulture].ToString());
            }
            else
            {
                Session[CommonContaist.CurrentCulture] = "vi";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi");
            }
        }
        /// <summary>
        /// đa ngôn ngữ
        /// </summary>
        /// <param name="ddlCulture"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        // changing culture
        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);

            Session[CommonContaist.CurrentCulture] = ddlCulture;
            return Redirect(returnUrl);
        }


        /// <summary>
        /// Kiểm tra đăng nhập
        /// </summary>
        /// <param name="fiterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext fiterContext)
        {
            var session = (UserLogin)Session[CommonContaist.USER_SESSION];
            if (session == null)
            {
                fiterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "login", action = "index", areas = "admin" }));
            }
            base.OnActionExecuting(fiterContext);
        }

        /// <summary>
        /// không báo với boostrap
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
    
}
