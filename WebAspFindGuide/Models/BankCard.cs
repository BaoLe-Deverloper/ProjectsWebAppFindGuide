namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankCard")]
    public partial class BankCard
    {
        public int BankCardID { get; set; }

        [StringLength(50)]
        public string BankCard_AccountID { get; set; }

        [StringLength(20)]
        public string BankCard_Number { get; set; }

        [StringLength(3)]
        public string BankCard_cnv { get; set; }

        public int? BankCard_BankID { get; set; }

        [StringLength(20)]
        public string BankCard_Type { get; set; }

        [StringLength(5)]
        public string BankCard_Time_lock { get; set; }

        public double? BankCard_RemainingAmount { get; set; }

        public virtual Account Account { get; set; }

        public virtual Bank Bank { get; set; }
    }
}
