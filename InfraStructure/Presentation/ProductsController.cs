using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstraction;
using Shared;
using Shared.Dtos.Products;
using Shared.ErrorModels;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailes))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailes))]
        [Cache(50)]
        public async Task<ActionResult> GEtAllProducts(ProductQueryParameters parameters)
        {
            var result = await _serviceManager.productServices.GetAllProductAsync( parameters);
            return Ok(result); //200

        }


        [HttpGet("{id}")] //Route : Url/api/product/id
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailes))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailes))]
        public async Task<ActionResult> GETProductById(int? id)
        {
            if(id is null) return BadRequest(); // 400

            var result = await _serviceManager.productServices.GetProductByIdAsync(id.Value);

            return Ok(result);
        }

        [HttpGet("brands")] // Url/api/Product/brand
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailes))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailes))]
        public async Task<IActionResult> GEtAllBrands()
        {
            var result = await _serviceManager.productServices.GetAllBrandsAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("types")] // Url/api/Product/types
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailes))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetailes))]
        public async Task<IActionResult> GETAllTypes()
        {
            var result = await _serviceManager.productServices.GetAllTypesAsync();
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
