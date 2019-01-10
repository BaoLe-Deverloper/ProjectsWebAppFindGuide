namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Language
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Language()
        {
            Account_Lauguages = new HashSet<Account_Lauguages>();
        }

        public int LanguageID { get; set; }

        [StringLength(50)]
        public string LanguageKey { get; set; }

        [StringLength(50)]
        public string LanguageName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account_Lauguages> Account_Lauguages { get; set; }
    }
}
