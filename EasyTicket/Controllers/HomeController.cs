using EasyTicket.Common;
using EasyTicket.Connection.DAO;
using EasyTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace  EasyTicket.Controllers
{
    public class HomeController : Controller
    {       
        public ActionResult Index()
        {
            var ticketDAO = new TicketDAO();
            ViewBag.Slides = new SlideDAO().ListAll();
            ViewBag.TicketTrend = ticketDAO.ListTrendTicket(6);
            ViewBag.TicketTopHot = ticketDAO.ListTopHotTicket(5);
            ViewBag.TicketMovie = ticketDAO.ListMovie(6);
            ViewBag.TicketMusic = ticketDAO.ListMusic(6);
            ViewBag.TicketDrama = ticketDAO.ListDrama(3);
            ViewBag.TicketSport = ticketDAO.ListSport(6);
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}