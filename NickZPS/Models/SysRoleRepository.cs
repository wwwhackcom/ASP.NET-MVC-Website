using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NickZPS.Models
{ 
    public class SysRoleRepository : ISysRoleRepository
    {
        NickZPSContext context = new NickZPSContext();

        public IQueryable<SysRole> All
        {
            get { return context.SysRoles; }
        }

        public IQueryable<SysRole> AllIncluding(params Expression<Func<SysRole, object>>[] includeProperties)
        {
            IQueryable<SysRole> query = context.SysRoles;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public SysRole Find(int id)
        {
            return context.SysRoles.Find(id);
        }

        public void InsertOrUpdate(SysRole sysrole)
        {
            if (sysrole.ID == default(int)) {
                // New entity
                context.SysRoles.Add(sysrole);
            } else {
                // Existing entity
                context.Entry(sysrole).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var sysrole = context.SysRoles.Find(id);
            context.SysRoles.Remove(sysrole);
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

    public interface ISysRoleRepository : IDisposable
    {
        IQueryable<SysRole> All { get; }
        IQueryable<SysRole> AllIncluding(params Expression<Func<SysRole, object>>[] includeProperties);
        SysRole Find(int id);
        void InsertOrUpdate(SysRole sysrole);
        void Delete(int id);
        void Save();
    }
}