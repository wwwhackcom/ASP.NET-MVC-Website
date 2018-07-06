using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NickZPS.Models
{ 
    public class FeedbackRepository : IFeedbackRepository
    {
        NickZPSContext context = new NickZPSContext();

        public IQueryable<Feedback> All
        {
            get { return context.Feedbacks; }
        }

        public IQueryable<Feedback> AllIncluding(params Expression<Func<Feedback, object>>[] includeProperties)
        {
            IQueryable<Feedback> query = context.Feedbacks;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Feedback Find(int id)
        {
            return context.Feedbacks.Find(id);
        }

        public void InsertOrUpdate(Feedback feedback)
        {
            if (feedback.ID == default(int)) {
                // New entity
                context.Feedbacks.Add(feedback);
            } else {
                // Existing entity
                context.Entry(feedback).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var feedback = context.Feedbacks.Find(id);
            context.Feedbacks.Remove(feedback);
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

    public interface IFeedbackRepository : IDisposable
    {
        IQueryable<Feedback> All { get; }
        IQueryable<Feedback> AllIncluding(params Expression<Func<Feedback, object>>[] includeProperties);
        Feedback Find(int id);
        void InsertOrUpdate(Feedback feedback);
        void Delete(int id);
        void Save();
    }
}