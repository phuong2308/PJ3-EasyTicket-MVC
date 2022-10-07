namespace EasyTicket.Connection.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Ticket")]
    public partial class Ticket
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public long? CategoryID { get; set; }

        public DateTime? WatchDate { get; set; }

        public DateTime? NextViewingDate { get; set; }

        [StringLength(250)]
        public string Artist { get; set; }

        public bool ShowOnHome { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
