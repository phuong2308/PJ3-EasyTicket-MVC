using EasyTicket.Connection.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyTicket.Connection.DAO
{
    public class OrderDAO
    {
        EasyTicketDbContext db = null;

        public OrderDAO()
        {
            db = new EasyTicketDbContext();
        }

        public long Insert(Order order)
        {
            db.Order.Add(order);
            db.SaveChanges();
            return order.ID;
        }

        public IEnumerable<Order> ListAllPaging(string searchString, int page = 1, int pageSize = 10)
        {
            IQueryable<Order> order = db.Order;
            if (!string.IsNullOrEmpty(searchString))
            {
                order = order.Where(x => x.Message.Contains(searchString));
            }
            return order.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
    }
}