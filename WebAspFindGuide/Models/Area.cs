namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Area
    {
        public Area()
        {
            Accounts = new HashSet<Account>();
        }

        public int AreaID { get; set; }

        [StringLength(50)]
        public string AreaName { get; set; }

        
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
