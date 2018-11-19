namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role
    {

        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public int RoleID { get; set; }

        [StringLength(30)]
        public string RoleKey { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }

      
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
