using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using NickZPS.Models;

namespace NickZPS.DAL
{
    public class AccountContext : DbContext
    {
        public AccountContext() : base("NickZPS")
        {

        }

        public DbSet <SysUser> SysUsers { get; set; }

        public DbSet <SysRole> SysRoles { get; set; }

        public DbSet <SysUserRole> SysUserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder moduleBuilder)
        {
            moduleBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}