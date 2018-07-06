using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NickZPS.Models
{
    [Serializable()]
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }

        [Required]
        public int UserID { get; set; }

        public DateTime InvoiceDate { get; set; }

        public Nullable<decimal> SubTotal { get; set; }

        public float Discount { get; set; }

        public virtual SysUser User { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}