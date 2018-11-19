namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Language
    {
     
        public Language()
        {
            Accounts = new HashSet<Account>();
        }

        public int LanguageID { get; set; }

        [StringLength(50)]
        public string LanguageKey { get; set; }

        [StringLength(50)]
        public string LanguageName { get; set; }

      
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
