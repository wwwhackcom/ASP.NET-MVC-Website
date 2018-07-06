using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NickZPS.Models
{ 
    public class SysUserRoleRepository : ISysUserRoleRepository
    {
        NickZPSContext context = new NickZPSContext();

        public IQueryable<SysUserRole> All
        {
            get { return context.SysUserRoles; }
        }

        public IQueryable<SysUserRole> AllIncluding(params Expression<Func<SysUserRole, object>>[] includeProperties)
        {
            IQueryable<SysUserRole> query = context.SysUserRoles;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public SysUserRole Find(int id)
        {
            return context.SysUserRoles.Find(id);
        }

        public void InsertOrUpdate(SysUserRole sysuserrole)
        {
            if (sysuserrole.ID == default(int)) {
                // New entity
                context.SysUserRoles.Add(sysuserrole);
            } else {
                // Existing entity
                context.Entry(sysuserrole).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var sysuserrole = context.SysUserRoles.Find(id);
            context.SysUserRoles.Remove(sysuserrole);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface ISysUserRoleRepository : IDisposable
    {
        IQueryable<SysUserRole> All { get; }
        IQueryable<SysUserRole> AllIncluding(params Expression<Func<SysUserRole, object>>[] includeProperties);
        SysUserRole Find(int id);
        void InsertOrUpdate(SysUserRole sysuserrole);
        void Delete(int id);
        void Save();
    }
}