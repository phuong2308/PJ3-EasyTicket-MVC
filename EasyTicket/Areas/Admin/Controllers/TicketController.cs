using EasyTicket.Connection.DAO;
using EasyTicket.Connection.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTicket.Areas.Admin.Controllers
{
    public class TicketController : BaseController
    {
        // GET: Admin/Ticket
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var ticketDAO = new TicketDAO();
            var ticket = ticketDAO.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(ticket);
        }

        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var ticketDAO = new TicketDAO();
                string fileName = Path.GetFileNameWithoutExtension(ticket.ImageFile.FileName);
                string extension = Path.GetExtension(ticket.ImageFile.FileName);
                fileName = fileName + extension;
                ticket.ImageUrl = "/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
                ticket.ImageFile.SaveAs(fileName);
                long id = ticketDAO.Insert(ticket);
                if (id > 0)
                {
                    SetAlert("Insert Ticket Successful", "success");
                    return RedirectToAction("Index", "Ticket");
                }
                else
                {
                    ModelState.AddModelError("", "Insert Ticket Failed");
                }
            }
            SetViewBag();
            return View("Index");
        }

        public ActionResult Edit(long id)
        {
            var ticket = new TicketDAO().ViewDetail(id);
            SetViewBag(ticket.CategoryID);
            return View(ticket);
        }

        [HttpPost]
        public ActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var ticketDAO = new TicketDAO();
                string fileName = Path.GetFileNameWithoutExtension(ticket.ImageFile.FileName);
                string extension = Path.GetExtension(ticket.ImageFile.FileName);
                fileName = fileName + extension;
                ticket.ImageUrl = "/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
                ticket.ImageFile.SaveAs(fileName);
                var result = ticketDAO.Update(ticket);
                if (result)
                {
                    SetAlert("Edit Ticket Successful", "success");
                    return RedirectToAction("Index", "Ticket");
                }
                else
                {
                    ModelState.AddModelError("","Edit Ticket Failed");
                }
            }
            SetViewBag();
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new TicketDAO().Delete(id);
            return RedirectToAction("Index");
        }

        public void SetViewBag(long? selectedId = null)
        {
            var categoryDAO = new CategoryDAO();
            ViewBag.CategoryID = new SelectList(categoryDAO.ListAll(), "ID", "Name", selectedId);
        }
    }
}