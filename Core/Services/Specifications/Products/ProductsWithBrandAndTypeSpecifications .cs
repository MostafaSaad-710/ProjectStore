using Domaine.Entities.Products;
using Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.Products
{
    public class ProductsWithBrandAndTypeSpecifications : BaseSpecifications<int, Product>
    {
        public ProductsWithBrandAndTypeSpecifications(ProductQueryParameters parameters) : base
            (
                P => (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId) 
                && 
                (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search))
            )
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);

            if (!string.IsNullOrEmpty(parameters.Sort))
            {
                // Check Value
                switch (parameters.Sort.ToLower())
                {
                    case "priceasc":
                        OrderBy = P => P.Price;
                        break;

                    case "pricedesc":
                        OrderByDescending = P => P.Price;
                        break;

                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }

            ApplyPagination(parameters.PageIndex, parameters.PageSize);
        }

       

        public ProductsWithBrandAndTypeSpecifications(int id) : base(P  => P.Id == id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
