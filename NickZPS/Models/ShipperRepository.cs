using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NickZPS.Models
{ 
    public class ShipperRepository : IShipperRepository
    {
        NickZPSContext context = new NickZPSContext();

        public IQueryable<Shipper> All
        {
            get { return context.Shippers; }
        }

        public IQueryable<Shipper> AllIncluding(params Expression<Func<Shipper, object>>[] includeProperties)
        {
            IQueryable<Shipper> query = context.Shippers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Shipper Find(int id)
        {
            return context.Shippers.Find(id);
        }

        public void InsertOrUpdate(Shipper shipper)
        {
            if (shipper.ShipperID == default(int)) {
                // New entity
                context.Shippers.Add(shipper);
            } else {
                // Existing entity
                context.Entry(shipper).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var shipper = context.Shippers.Find(id);
            context.Shippers.Remove(shipper);
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

    public interface IShipperRepository : IDisposable
    {
        IQueryable<Shipper> All { get; }
        IQueryable<Shipper> AllIncluding(params Expression<Func<Shipper, object>>[] includeProperties);
        Shipper Find(int id);
        void InsertOrUpdate(Shipper shipper);
        void Delete(int id);
        void Save();
    }
}