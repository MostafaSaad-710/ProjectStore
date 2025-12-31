using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine.Contracts
{
    public interface IUnitofwork 
    {
        IGenericRepository<TKey , TEntity> GetRepositoryAsync<TKey, TEntity>() where TEntity : BaseEntity<TKey>;
        Task<int> SaveChangesAsync();
        
    }
}
