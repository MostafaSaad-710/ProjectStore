using Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Products
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductResponse>> GetAllProductAsync();
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();

    }
}
