using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domaine.Contracts
{
    public interface  ISpecifications<TKey ,TEntity> where TEntity : BaseEntity<TKey>
    {
         List<Expression<Func<TEntity, object>>> Includes { get; set; }
         Expression<Func<TEntity,bool>>? Criteria { get; set; }

         Expression<Func<TEntity, Object>>? OrderBy { get; set; }
         Expression<Func<TEntity, Object>>? OrderByDescending { get; set; }

         int Skip { get; set; }
         int Take { get; set; }
         bool IsPaginatoin { get; set; }
    }    
}
