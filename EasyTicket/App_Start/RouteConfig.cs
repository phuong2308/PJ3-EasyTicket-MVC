using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EasyTicket
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Ticket Category",
                url: "product/{metatitle}-{id}",
                defaults: new
                {
                    controller = "Ticket",
                    action = "CategoryTicket",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Ticket Detail",
                url: "detail/{metatitle}-{id}",
                defaults: new
                {
                    controller = "Ticket",
                    action = "TicketDetail",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "cart",
                defaults: new
                {
                    controller = "Cart",
                    action = "Index",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Add Cart",
                url: "add-cart",
                defaults: new
                {
                    controller = "Cart",
                    action = "AddItem",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Payment",
                url: "payment",
                defaults: new
                {
                    controller = "Cart",
                    action = "Payment",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Payment Success",
                url: "success",
                defaults: new
                {
                    controller = "Cart",
                    action = "Success",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new
                {
                    controller = "User",
                    action = "Register",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new
                {
                    controller = "User",
                    action = "Login",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "logout",
                defaults: new
                {
                    controller = "User",
                    action = "Logout",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Search",
                url: "search",
                defaults: new
                {
                    controller = "Ticket",
                    action = "Search",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "FAQ",
                url: "faq",
                defaults: new
                {
                    controller = "FAQ",
                    action = "Index",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "contact",
                defaults: new
                {
                    controller = "Contact",
                    action = "Index",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                }, namespaces: new[] { "EasyTicket.Controllers" }
            );
        }
    }
}
