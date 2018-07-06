using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NickZPS.Models
{ 
    public class OrderDetailRepository : IOrderDetailRepository
    {
        NickZPSContext context = new NickZPSContext();

        public IQueryable<OrderDetail> All
        {
            get { return context.OrderDetails; }
        }

        public IQueryable<OrderDetail> AllIncluding(params Expression<Func<OrderDetail, object>>[] includeProperties)
        {
            IQueryable<OrderDetail> query = context.OrderDetails;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderDetail Find(int id)
        {
            return context.OrderDetails.Find(id);
        }

        public void InsertOrUpdate(OrderDetail orderdetail)
        {
            if (orderdetail.ID == default(int)) {
                // New entity
                context.OrderDetails.Add(orderdetail);
            } else {
                // Existing entity
                context.Entry(orderdetail).State = System.Data.Entity.EntityState.Modified;
            }
        }
        public IEnumerable<OrderDetail> Where(string sql)
        {
            return context.Database.SqlQuery<OrderDetail>(sql);
        }

        public void Delete(int id)
        {
            var orderdetail = context.OrderDetails.Find(id);
            context.OrderDetails.Remove(orderdetail);
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

    public interface IOrderDetailRepository : IDisposable
    {
        IQueryable<OrderDetail> All { get; }
        IQueryable<OrderDetail> AllIncluding(params Expression<Func<OrderDetail, object>>[] includeProperties);
        OrderDetail Find(int id);
        void InsertOrUpdate(OrderDetail orderdetail);
        void Delete(int id);
        void Save();
        IEnumerable<OrderDetail> Where(string sql);
    }
}