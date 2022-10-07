using EasyTicket.Connection.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyTicket.Models
{
    [Serializable]
    public class CartItem
    {
        public Ticket Ticket { get; set; }
        public int Quantity { get; set; }

    }
}