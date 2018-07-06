using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NickZPS.Models
{ 
    public class SysUserRepository : ISysUserRepository
    {
        NickZPSContext context = new NickZPSContext();

        public IQueryable<SysUser> All
        {
            get { return context.SysUsers; }
        }

        public IQueryable<SysUser> AllIncluding(params Expression<Func<SysUser, object>>[] includeProperties)
        {
            IQueryable<SysUser> query = context.SysUsers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public SysUser FindByEmail(string email)
        {
            return context.SysUsers.FirstOrDefault(u => u.Email == email);
        }

        public SysUser Find(int id)
        {
            return context.SysUsers.Find(id);
        }

        public void InsertOrUpdate(SysUser sysuser)
        {
            if (sysuser.ID == default(int)) {
                // New entity
                context.SysUsers.Add(sysuser);
            } else {
                // Existing entity
                context.Entry(sysuser).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var sysuser = context.SysUsers.Find(id);
            context.SysUsers.Remove(sysuser);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }

        public IQueryable<SysUser> Login(string email, string password)
        {
            return context.SysUsers.Where(u => u.Email == email && u.Password == password);
        }
    }

    public interface ISysUserRepository : IDisposable
    {
        IQueryable<SysUser> All { get; }
        IQueryable<SysUser> AllIncluding(params Expression<Func<SysUser, object>>[] includeProperties);
        SysUser Find(int id);
        void InsertOrUpdate(SysUser sysuser);
        void Delete(int id);
        void Save();
        IQueryable<SysUser> Login(string email, string password);
        SysUser FindByEmail(string email);
    }
}