namespace WebAspFindGuide.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public int MessageID { get; set; }

        [Required]
        [StringLength(50)]
        public string Message_AccountID_From { get; set; }

        [Required]
        [StringLength(50)]
        public string Message_AccountID_To { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Message_TimeToSend { get; set; }

        [Column(TypeName = "text")]
        public string Message_Content { get; set; }

        public bool? Message_Status { get; set; }

        public virtual Account Account { get; set; }

        public virtual Account Account1 { get; set; }
    }
}
