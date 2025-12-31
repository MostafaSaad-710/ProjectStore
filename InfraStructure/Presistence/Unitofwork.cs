using Domaine.Contracts;
using Domaine.Entities;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Unitofwork(StoreDbContext _context) : IUnitofwork
    {
        //private Dictionary<string, Object> _repository = new Dictionary<string, Object>();
        //public  IGenericRepository<TKey, TEntity> GetRepositoryAsync<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        //{
        //    string type = typeof(TEntity).Name;

        //    if (!_repository.ContainsKey(type))
        //    {
        //        _repository.Add(type, new GenericRepository<TKey, TEntity>(_context) );
        //    }

        //    return (IGenericRepository<TKey, TEntity>) _repository[type];
        //}

        private ConcurrentDictionary<string, Object> _repository = new ConcurrentDictionary<string, Object>();

        public IGenericRepository<TKey, TEntity> GetRepositoryAsync<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        {  
            return (IGenericRepository<TKey, TEntity>)_repository.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TKey, TEntity>(_context));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
