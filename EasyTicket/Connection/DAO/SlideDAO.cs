using EasyTicket.Connection.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyTicket.Connection.DAO
{
    public class SlideDAO
    {
        EasyTicketDbContext db = null;

        public SlideDAO()
        {
            db = new EasyTicketDbContext();
        }

        public List<Slide> ListAll()
        {
            return db.Slide.OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}