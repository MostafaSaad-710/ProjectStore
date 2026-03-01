using AutoMapper;
using Domaine.Contracts;
using Domaine.Entities.Baskets;
using Domaine.Exeptions.BadRequest;
using Domaine.Exeptions.NotFound;
using Services.Abstraction.Baskets;
using Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Baskets
{
    public class BasketServices(IBasketRepository _basketRepository , IMapper _mapper) : IBasketServices
    {
        public async Task<BasketDto?> GetBasketAsync(string id)
        { 
            var basket = await _basketRepository.GetBasketAsync(id);

            if (basket is null) throw new BasketNotFoundExeptoin(id);

            var result = _mapper.Map<BasketDto>(basket);
             
            return result;
        }

        public async Task<BasketDto?> CreateBasketAsync(BasketDto dto, TimeSpan duration)
        {
            var basket =  _mapper.Map<CustomerBasket>(dto);

            var result = await _basketRepository.CreateBasketAsync(basket, duration);

            if (result is null) throw new CreateOrUpdateBasketBadRequestExeption();

            return _mapper.Map<BasketDto>(result);
        }


        public async Task<bool?> DeleteBasketAsync(string id)
        {
            var flag = await _basketRepository.DeleteBasketAsync(id);

            if (!flag) throw new DeleteBasketBadRequestExeption();

            return flag;
        }


    }
}
