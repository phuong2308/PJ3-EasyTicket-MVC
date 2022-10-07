using EasyTicket.Connection.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTicket.Areas.Admin.Controllers
{
    public class OrderDetailController : BaseController
    {
        // GET: Admin/OrderDetail
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var orderDetailDAO = new OrderDetailDAO();
            var orderDetail = orderDetailDAO.ListAllPaging(page, pageSize);
            return View(orderDetail);
        }
    }
}