using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyTicket.Connection.ViewModel
{
    public class TicketViewModel
    {
        public long ID { get; set; }
        public string Images { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string CategoryName { get; set; }
        public string MetaTitle { get; set; }
        public DateTime? WatchDate { get; set; }
        public int? Quantity { get; set; }
    }
}