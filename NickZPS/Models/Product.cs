using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NickZPS.Models
{
    [Serializable()]
    public class Product
    {
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public int CategoryID { get; set; }

        public string QuantityPerUnit { get; set; }

        public Nullable<decimal> UnitPrice { get; set; }

        public Nullable<short> UnitsInStock { get; set; }

        public Nullable<short> UnitsOnOrder { get; set; }

        public Nullable<short> ReorderLevel { get; set; }
        
        public bool Discontinued { get; set; }

        public bool IsHome { get; set; }

        public string ProductPoster { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}