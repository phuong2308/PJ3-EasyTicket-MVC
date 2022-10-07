using EasyTicket.Connection.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyTicket.Connection.DAO
{
    public class UserGroupDAO
    {
        EasyTicketDbContext db = null;

        public UserGroupDAO()
        {
            db = new EasyTicketDbContext();
        }

        public List<UserGroup> ListAll()
        {
            return db.UserGroup.ToList();
        }
    }
}