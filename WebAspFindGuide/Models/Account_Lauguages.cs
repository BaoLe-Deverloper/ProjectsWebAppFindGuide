namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account_Lauguages
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountID { get; set; }

        public int LanguageID { get; set; }

        public virtual Account Account { get; set; }

        public virtual Language Language { get; set; }
    }
}
