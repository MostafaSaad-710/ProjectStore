using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await _serviceManager.BasketServices.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket(BasketDto dto)
        {
            var result = await _serviceManager.BasketServices.CreateBasketAsync(dto, TimeSpan.FromDays(1));
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            var result =  await _serviceManager.BasketServices.DeleteBasketAsync(id);
            return NoContent(); // 204
        }

    }
}
