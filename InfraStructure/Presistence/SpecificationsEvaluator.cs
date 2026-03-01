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



            // Check Criteria To Filter

            if (spec.Criteria is not null)
            {
                query = inputquery.Where(spec.Criteria); // _context.Products.Where(P => P.Id == 12).FirstOr
            }

            // Check Expression Which To Order By With
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }


            if(spec.IsPaginatoin)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query , (query , includeExpresstion ) => query.Include(includeExpresstion));

            return query;
        }
    }
}
