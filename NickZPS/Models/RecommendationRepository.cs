using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NickZPS.Models
{ 
    public class RecommendationRepository : IRecommendationRepository
    {
        NickZPSContext context = new NickZPSContext();

        public IQueryable<Recommendation> All
        {
            get { return context.Recommendations; }
        }

        public IQueryable<Recommendation> AllIncluding(params Expression<Func<Recommendation, object>>[] includeProperties)
        {
            IQueryable<Recommendation> query = context.Recommendations;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Recommendation Find(int id)
        {
            return context.Recommendations.Find(id);
        }

        public void InsertOrUpdate(Recommendation recommendation)
        {
            if (recommendation.ID == default(int)) {
                // New entity
                context.Recommendations.Add(recommendation);
            } else {
                // Existing entity
                context.Entry(recommendation).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public IEnumerable<Recommendation> Where(string sql)
        {
            return context.Database.SqlQuery<Recommendation>(sql);
        }

        public void Delete(int id)
        {
            var recommendation = context.Recommendations.Find(id);
            context.Recommendations.Remove(recommendation);
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

    public interface IRecommendationRepository : IDisposable
    {
        IQueryable<Recommendation> All { get; }
        IQueryable<Recommendation> AllIncluding(params Expression<Func<Recommendation, object>>[] includeProperties);
        Recommendation Find(int id);
        void InsertOrUpdate(Recommendation recommendation);
        void Delete(int id);
        void Save();
        IEnumerable<Recommendation> Where(string sql);
    }
}