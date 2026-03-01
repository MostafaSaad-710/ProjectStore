using AutoMapper;
using Domaine.Contracts;
using Services.Abstraction;
using Services.Abstraction.Baskets;
using Services.Abstraction.Cache;
using Services.Abstraction.Products;
using Services.Baskets;
using Services.Cache;
using Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitofwork _unitofwork, IMapper _mapper , IBasketRepository _basketRepository , ICacheRepository _cacheRepository) : IServiceManager
    {
        public IProductServices productServices { get; } = new ProductServices(_unitofwork , _mapper);

        public IBasketServices BasketServices { get; } = new BasketServices(_basketRepository , _mapper);

        public ICacheService cacheService { get; } = new CacheService(_cacheRepository);
    }
}
