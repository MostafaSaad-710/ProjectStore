using Domaine.Contracts;
using Services.Abstraction.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Cache
{
    internal class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string key)
        {
            var result =  await _cacheRepository.GetAsync(key);
            return result;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
             await _cacheRepository.SetAsync(key, value, duration);
        }
    }
}
