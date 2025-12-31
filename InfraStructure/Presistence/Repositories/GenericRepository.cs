using Domaine.Contracts;
using Domaine.Entities;
using Domaine.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TKey, TEntity>(StoreDbContext _dbContext) : IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool ChangeTracker = false)
        {
            return ChangeTracker ?
                await _dbContext.Set<TEntity>().ToListAsync() 
                : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
 
        }

        public async Task<TEntity?> GetAsync(TKey key)
        {
            if(typeof(TEntity) == typeof(Product))
            {
                return await _dbContext.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P => P.Id == key as int?) as TEntity;

            }
            return await _dbContext.Set<TEntity>().FindAsync(key); 
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
             _dbContext.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

       

       
    }
}
