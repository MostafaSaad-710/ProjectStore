using Domaine.Entities.Products;
using Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.Products
{
    public class ProductsCountSpecifications : BaseSpecifications<int , Product>
    {
        public ProductsCountSpecifications(ProductQueryParameters parameters) : base
            (
                P => (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId)
                &&
                (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search))
            )
        {

        }
    }
}
