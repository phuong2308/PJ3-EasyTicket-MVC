using EasyTicket.Connection.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyTicket.Connection.DAO
{
    public class OrderDetailDAO
    {
        EasyTicketDbContext db = null;

        public OrderDetailDAO()
        {
            db = new EasyTicketDbContext();
        }

        public bool Insert(OrderDetail orderDetail)
        {
            try
            {
                db.OrderDetail.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<OrderDetail> ListAllPaging(int page = 1, int pageSize = 10)
        {
            IQueryable<OrderDetail> orderDetail = db.OrderDetail;
            return orderDetail.OrderBy(x => x.OrderID).ToPagedList(page, pageSize);
        }
    }
}