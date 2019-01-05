using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAspFindGuide.Areas.Admin_Page.Models.Objects
{
        [Table("Admin_Account")]
        public class Admin_Account
        {
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
    }
}