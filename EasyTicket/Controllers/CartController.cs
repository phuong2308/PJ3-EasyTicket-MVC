using EasyTicket.Common;
using EasyTicket.Connection.DAO;
using EasyTicket.Connection.EF;
using EasyTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace EasyTicket.Controllers
{
    public class CartController : BaseController
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Ticket.ID == item.Ticket.ID);
                if(jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }             
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Ticket.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult AddItem(long ticketId, int quantity)
        {
            var ticket = new TicketDAO().ViewDetail(ticketId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Ticket.ID == ticketId))
                {
                    foreach (var item in list)
                    {
                        if (item.Ticket.ID == ticketId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Ticket = ticket;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Ticket = ticket;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string message)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var order = new Order();
            order.OrderDate = DateTime.Now;
            order.Message = message;
            order.UserID = session.UserID;
            try
            {
                var id = new OrderDAO().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var orderDetailDAO = new OrderDetailDAO();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.TicketID = item.Ticket.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Ticket.Price;
                    orderDetail.Quantity = item.Quantity;
                    orderDetailDAO.Insert(orderDetail);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return Redirect("/success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}