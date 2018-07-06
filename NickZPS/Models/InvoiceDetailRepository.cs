using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NickZPS.Models
{ 
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        NickZPSContext context = new NickZPSContext();

        public IQueryable<InvoiceDetail> All
        {
            get { return context.InvoiceDetails; }
        }

        public IQueryable<InvoiceDetail> AllIncluding(params Expression<Func<InvoiceDetail, object>>[] includeProperties)
        {
            IQueryable<InvoiceDetail> query = context.InvoiceDetails;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public InvoiceDetail Find(int id)
        {
            return context.InvoiceDetails.Find(id);
        }

        public void InsertOrUpdate(InvoiceDetail invoicedetail)
        {
            if (invoicedetail.ID == default(int)) {
                // New entity
                context.InvoiceDetails.Add(invoicedetail);
            } else {
                // Existing entity
                context.Entry(invoicedetail).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var invoicedetail = context.InvoiceDetails.Find(id);
            context.InvoiceDetails.Remove(invoicedetail);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IEnumerable<InvoiceDetail> Where(string sql)
        {
            return context.Database.SqlQuery<InvoiceDetail>(sql);
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IInvoiceDetailRepository : IDisposable
    {
        IQueryable<InvoiceDetail> All { get; }
        IQueryable<InvoiceDetail> AllIncluding(params Expression<Func<InvoiceDetail, object>>[] includeProperties);
        InvoiceDetail Find(int id);
        void InsertOrUpdate(InvoiceDetail invoicedetail);
        void Delete(int id);
        void Save();
        IEnumerable<InvoiceDetail> Where(string sql);
    }
}