using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Controller]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet] //Route: BaseURL/api/product
        public async Task<IActionResult> GEtAllProducts()
        {
            var result = await _serviceManager.productServices.GetAllProductAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result); //200

        }
        [HttpGet("{id}")] //Route : Url/api/product/id
        public async Task<IActionResult> GETProductById(int? id)
        {
            if(id is null) return BadRequest(); // 400

            var result = await _serviceManager.productServices.GetProductByIdAsync(id.Value);
            if (result is null) return NotFound(); //404
            return Ok(result);
        }

        [HttpGet("brands")] // Url/api/Product/brand
        public async Task<IActionResult> GEtAllBrands()
        {
            var result = await _serviceManager.productServices.GetAllBrandsAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("types")] // Url/api/Product/types
        public async Task<IActionResult> GETAllTypes()
        {
            var result = await _serviceManager.productServices.GetAllTypesAsync();
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
