using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NickZPS.Models
{
    [Serializable()]
    public class CartProduct
    {
        public CartProduct(Product prouduct)
        {
            this.prouduct = prouduct;
        }
        public int Quan { get; set; }
        public Product prouduct { get; set; }
    }
}