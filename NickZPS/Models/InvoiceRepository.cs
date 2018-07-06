using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NickZPS.Models
{ 
    public class InvoiceRepository : IInvoiceRepository
    {
        NickZPSContext context = new NickZPSContext();

        public IQueryable<Invoice> All
        {
            get { return context.Invoices; }
        }

        public IQueryable<Invoice> AllIncluding(params Expression<Func<Invoice, object>>[] includeProperties)
        {
            IQueryable<Invoice> query = context.Invoices;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Invoice Find(int id)
        {
            return context.Invoices.Find(id);
        }

        public IEnumerable<Invoice> Where(string sql)
        {
            return context.Database.SqlQuery<Invoice>(sql);
        }

        public void InsertOrUpdate(Invoice invoice)
        {
            if (invoice.InvoiceID == default(int)) {
                // New entity
                context.Invoices.Add(invoice);
            } else {
                // Existing entity
                context.Entry(invoice).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var invoice = context.Invoices.Find(id);
            context.Invoices.Remove(invoice);
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

    public interface IInvoiceRepository : IDisposable
    {
        IQueryable<Invoice> All { get; }
        IQueryable<Invoice> AllIncluding(params Expression<Func<Invoice, object>>[] includeProperties);
        Invoice Find(int id);
        void InsertOrUpdate(Invoice invoice);
        void Delete(int id);
        void Save();
        IEnumerable<Invoice> Where(string sql);
    }
}