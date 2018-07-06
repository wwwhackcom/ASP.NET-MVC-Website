using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NickZPS.Models
{ 
    public class OrderRepository : IOrderRepository
    {
        NickZPSContext context = new NickZPSContext();

        public IQueryable<Order> All
        {
            get { return context.Orders; }
        }

        public IQueryable<Order> AllIncluding(params Expression<Func<Order, object>>[] includeProperties)
        {
            IQueryable<Order> query = context.Orders;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Order Find(int id)
        {
            return context.Orders.Find(id);
        }

        public void InsertOrUpdate(Order order)
        {
            if (order.OrderID == default(int)) {
                // New entity
                context.Orders.Add(order);
            } else {
                // Existing entity
                context.Entry(order).State = System.Data.Entity.EntityState.Modified;
            }
        }
        public IEnumerable<Order> Where(string sql)
        {
            return context.Database.SqlQuery<Order>(sql);
        }
        public void Delete(int id)
        {
            var order = context.Orders.Find(id);
            context.Orders.Remove(order);
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

    public interface IOrderRepository : IDisposable
    {
        IQueryable<Order> All { get; }
        IQueryable<Order> AllIncluding(params Expression<Func<Order, object>>[] includeProperties);
        Order Find(int id);
        void InsertOrUpdate(Order order);
        void Delete(int id);
        void Save();
        IEnumerable<Order> Where(string sql);
    }
}