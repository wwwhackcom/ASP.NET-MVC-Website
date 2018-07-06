using System;
using System.ComponentModel.DataAnnotations;

namespace NickZPS.Models
{
    [Serializable()]
    public class InvoiceDetail
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int InvoiceID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public short Quantity { get; set; }

        public float Discount { get; set; }

        public DateTime OrderDate { get; set; }

        public string ProductName { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}