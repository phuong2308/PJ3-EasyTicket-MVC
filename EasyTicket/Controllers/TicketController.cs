using EasyTicket.Connection.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTicket.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Category()
        {
            var categoryDAO = new CategoryDAO().ListAll();
            return PartialView(categoryDAO);
        }

        public ActionResult CategoryTicket(long id, int page = 1, int pageSize = 9)
        {
            var ticketDAO = new TicketDAO();
            ViewBag.TicketTopHot = ticketDAO.ListTopHotTicket(5);
            var category = new CategoryDAO().ViewDetail(id);
            ViewBag.CategoryTicket = category;
            int totalRecord = 0;
            var ticket = new TicketDAO().ListByCategoryID(id, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(ticket);
        }

        public ActionResult TicketDetail(long id)
        {
            var ticket = new TicketDAO().ViewDetail(id);
            ViewBag.Category = new CategoryDAO().ViewDetail(ticket.CategoryID.Value);
            ViewBag.FeatureTicket = new TicketDAO().ListFeatureTicket(id,4);
            return View(ticket);
        }

        public JsonResult ListName(string term)
        {
            var data = new TicketDAO().ListName(term);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, int pageSize = 9)
        {
            var ticketDAO = new TicketDAO();
            ViewBag.TicketTopHot = ticketDAO.ListTopHotTicket(5);

            int totalRecord = 0;
            var ticket = new TicketDAO().Search(keyword, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(ticket);
        }
    }
}