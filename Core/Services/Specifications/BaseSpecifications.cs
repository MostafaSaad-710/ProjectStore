using Domaine.Contracts;
using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class BaseSpecifications<TKey, TEntity> : ISpecifications<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> Includes { get ; set ; } = new List<Expression<Func<TEntity, object>>> ();
        public Expression<Func<TEntity, bool>>? Criteria { get ; set; }
        public  Expression<Func<TEntity, object>>? OrderBy { get ; set ; }
        public  Expression<Func<TEntity, object>>? OrderByDescending { get ; set ; }
        public int Skip { get; set; }
        public int Take { get; set ; }
        public bool IsPaginatoin { get ; set ; }



        public void ApplyPagination(int pageindex, int pagesize)
        {
            Skip = (pageindex - 1) * pagesize;
            Take = pagesize;
            IsPaginatoin = true;
        }


        public BaseSpecifications( Expression<Func<TEntity, bool>>? expression)
        {
            Criteria = expression;
        }


        public void AddOrderBy(Expression<Func<TEntity, object>>? Expression)
        {
            OrderBy = Expression;
        }
    }
}
