namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderTour")]
    public partial class OrderTour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderTour()
        {
            Billing_App = new HashSet<Billing_App>();
        }

        public int OrderTourID { get; set; }

        public int? OrderTour_Guide_ID { get; set; }

        public int? OrderTour_Tourists_ID { get; set; }

        public double? OrderTour_Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OrderTour_StartTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OrderTour_EndTime { get; set; }

        [StringLength(1)]
        public string OrderTour_Status { get; set; }

        public int? OrderTour_Rate { get; set; }

        [StringLength(500)]
        public string OrderTour_RateComment { get; set; }

        public virtual Account Account { get; set; }

        public virtual Account Account1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Billing_App> Billing_App { get; set; }
    }
}
