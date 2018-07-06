using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NickZPS.Models
{
    public class Recommendation
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ProductID { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public int Star { get; set; }
    }
}