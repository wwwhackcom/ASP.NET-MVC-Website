using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NickZPS.Models
{
    public class NickZPSContext : DbContext
    {
        public NickZPSContext() : base("NickZPS")
        {

        }

        protected override void OnModelCreating(DbModelBuilder moduleBuilder)
        {
            moduleBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<NickZPS.Models.Category> Categories { get; set; }

        public DbSet<NickZPS.Models.Product> Products { get; set; }

        public DbSet<NickZPS.Models.Shipper> Shippers { get; set; }

        public DbSet<NickZPS.Models.SysRole> SysRoles { get; set; }

        public DbSet<NickZPS.Models.SysUserRole> SysUserRoles { get; set; }

        public DbSet<NickZPS.Models.Order> Orders { get; set; }

        public DbSet<NickZPS.Models.OrderDetail> OrderDetails { get; set; }

        public DbSet<NickZPS.Models.Invoice> Invoices { get; set; }

        public DbSet<NickZPS.Models.InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<NickZPS.Models.SysUser> SysUsers { get; set; }

        public DbSet<NickZPS.Models.Feedback> Feedbacks { get; set; }

        public DbSet<NickZPS.Models.Recommendation> Recommendations { get; set; }
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<NickZPS.Models.NickZPSContext>());

    }
}