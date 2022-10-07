namespace EasyTicket.Connection.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public long ID { get; set; }

        public DateTime? OrderDate { get; set; }

        public bool? Status { get; set; }

        public long? UserID { get; set; }

        [StringLength(250)]
        public string Message { get; set; }
    }
}
