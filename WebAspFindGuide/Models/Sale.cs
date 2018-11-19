namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sale")]
    public partial class Sale
    {
      
        public Sale()
        {
            Billing_App = new HashSet<Billing_App>();
        }

        [Key]
        [StringLength(20)]
        public string Sale_Key { get; set; }

        public int? Sale_Percent { get; set; }

        public bool Sale_Status { get; set; }
        
        public virtual ICollection<Billing_App> Billing_App { get; set; }
    }
}
