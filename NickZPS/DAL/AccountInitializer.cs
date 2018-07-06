using NickZPS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NickZPS.DAL
{
    public class AccountInitializer : DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            var sysUsers = new List<SysUser>
            {
                new SysUser{ UserName="Admin", Email="admin@zps.co.nz", Password="admin" },
                new SysUser{ UserName="Guest", Email="guest@zps.co.nz", Password="1234" }
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();

            var sysRoles = new List<SysRole>
            {
                new SysRole{ RoleName="Admin", RoleDesc="Administrator" },
                new SysRole{ RoleName="General User", RoleDesc="General Users" }
            };
            sysRoles.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();

            var sysUserRoles = new List<SysUserRole>
            {
                new SysUserRole{ SysUserID=1, SysRoleId=1 },
                new SysUserRole{ SysUserID=1, SysRoleId=2 },
                new SysUserRole{ SysUserID=2, SysRoleId=2 }
            };
            sysUserRoles.ForEach(s => context.SysUserRoles.Add(s));
            context.SaveChanges();
        }
    }
}