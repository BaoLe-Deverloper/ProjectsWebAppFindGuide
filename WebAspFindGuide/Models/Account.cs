namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Account_Lauguages = new HashSet<Account_Lauguages>();
            BankCards = new HashSet<BankCard>();
            Messages = new HashSet<Message>();
            Messages1 = new HashSet<Message>();
            OrderTours = new HashSet<OrderTour>();
            OrderTours1 = new HashSet<OrderTour>();
        }

        [StringLength(50)]
        public string AccountID { get; set; }

        [Required]
        [StringLength(50)]
        public string Account_Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Account_Pass { get; set; }

        [Required]
        [StringLength(50)]
        public string Account_Email { get; set; }

        [StringLength(50)]
        public string Account_Facebook { get; set; }

        public int? Account_RoleID { get; set; }

        public bool? Account_Gender { get; set; }

        [StringLength(20)]
        public string Account_Phone { get; set; }

        [StringLength(200)]
        public string Account_Address { get; set; }

        [StringLength(100)]
        public string Account_Avarta { get; set; }

        public int? Acccount_Point { get; set; }

        [Column(TypeName = "xml")]
        public string Accout_Image_more { get; set; }

        [Column(TypeName = "xml")]
        public string Account_Info_more { get; set; }

        [Column(TypeName = "xml")]
        public string Account_Info_Schedule { get; set; }

        public bool? Account_Config { get; set; }

        public bool? Account_Lock { get; set; }

        public int? Account_Area { get; set; }

        [StringLength(200)]
        public string Account_CodeConfig { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Account_CreateDate { get; set; }

        public virtual Area Area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account_Lauguages> Account_Lauguages { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankCard> BankCards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTour> OrderTours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTour> OrderTours1 { get; set; }
    }
}
