namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bank()
        {
            BankCards = new HashSet<BankCard>();
        }

        public int BankID { get; set; }

        [StringLength(20)]
        public string Bank_Name { get; set; }

        [StringLength(50)]
        public string Bank_Swift { get; set; }

        [StringLength(20)]
        public string Bank_Intermediate { get; set; }

        [StringLength(50)]
        public string Bank_Swift_imd { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankCard> BankCards { get; set; }
    }
}
