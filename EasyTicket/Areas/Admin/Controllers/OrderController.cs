using EasyTicket.Connection.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTicket.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var orderDAO = new OrderDAO();
            var order = orderDAO.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(order);
        }
    }
}