using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NickZPS.Models
{
    [Serializable()]
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public short Quantity { get; set; }

        public float Discount { get; set; }

        public string ProductName { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}