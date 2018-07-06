using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NickZPS.Models
{
    [Serializable()]
    public class SysUserRole
    {
        public int ID { get; set; }
        public int SysUserID { get; set; }
        public int SysRoleId { get; set; }
        public virtual SysUser SysUser { get; set;}

        public virtual SysRole SysRole { get; set; }
    }
}