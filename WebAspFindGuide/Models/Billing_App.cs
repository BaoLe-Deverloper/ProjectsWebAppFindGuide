namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Billing_App
    {
        [Key]
        public int BillingApp_ID { get; set; }

        public int? BillingApp_TourID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BillingApp_Date { get; set; }

        public bool? BillingApp_Status { get; set; }

        public double? BillingApp_Amount { get; set; }

        [StringLength(20)]
        public string BllingApp_KeySale { get; set; }

        public virtual OrderTour OrderTour { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
