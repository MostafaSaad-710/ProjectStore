using Domaine.Contracts;
using Domaine.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TKey,TEntity>(IQueryable<TEntity> inputquery , ISpecifications<TKey, TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            IQueryable<TEntity> query = inputquery;

            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            spec.Includes.Aggregate(query , (query , includeExpresstion ) => query.Include(includeExpresstion));

            return query;
        }
    }
}
