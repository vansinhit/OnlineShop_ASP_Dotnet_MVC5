using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}",
    new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            
            
            
            routes.MapRoute(
                name: "ProductCategory",
                url: "san-pham/{metatile}-{Caid}",
                defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
           name: "Product",
           url: "chi-tiet/{metatitle}-{idproduct}",
           defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional },
           namespaces: new[] { "OnlineShop.Controllers" }
       );
            routes.MapRoute(
              name: "Catr",
              url: "gio-hang",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
          );
            routes.MapRoute(
              name: "Register",
              url: "dang-ki",
              defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
          );
            routes.MapRoute(
              name: "Login",
              url: "dang-nhap",
              defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
          );
            routes.MapRoute(
          name: "Payment  success",
          url: "hoan-thanh",
          defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
          namespaces: new[] { "OnlineShop.Controllers" }
      );
            routes.MapRoute(
              name: "Payment",
              url: "thanh-toan",
              defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
          );
            routes.MapRoute(
                name: "CartAdd",
                url: "them-vao-gio",
                defaults: new { controller = "Cart", action = "AddCart", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "ListProduct",
                url: "san-pham",
                defaults: new { controller = "ListProduct", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );
        }
    }
}
