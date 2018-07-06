using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NickZPS.Models
{
    [Serializable()]
    public class SysUser
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public Nullable<DateTime> DOB { get; set; }

        public string Desc { get; set; }

        public string AvararPath { get; set; }

        public int HearAbout { get; set; }

        public bool IsInactive { get; set; }

        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}