using Services.Abstraction.Baskets;
using Services.Abstraction.Cache;
using Services.Abstraction.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
         IProductServices productServices { get; }
         IBasketServices BasketServices { get; }
        ICacheService  cacheService { get; }
    }
}
