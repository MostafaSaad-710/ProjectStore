using AutoMapper;
using Domaine.Contracts;
using Domaine.Entities;
using Domaine.Entities.Products;
using Domaine.Exeptions.NotFound;
using Services.Abstraction.Products;
using Services.Specifications;
using Services.Specifications.Products;
using Shared;
using Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products;  
public class ProductServices(IUnitofwork _unitofwork , IMapper _mapper) : IProductServices
{ 
    public async Task<PaginationResponse<ProductResponse>> GetAllProductAsync(ProductQueryParameters parameters)
    {
        //var spec = new BaseSpecifications<int, Product>(null);

        //spec.Includes.Add(P => P.Brand);
        //spec.Includes.Add(P => P.Type);

        var spec = new ProductsWithBrandAndTypeSpecifications(parameters);

        var products = await _unitofwork.GetRepositoryAsync<int, Product>().GetAllAsync(spec); 
        var result = _mapper.Map<IEnumerable<ProductResponse>>(products);

        var countSpec = new ProductsCountSpecifications(parameters);

        var count = await _unitofwork.GetRepositoryAsync<int, Product>().GetCountAsync(countSpec);

        return new PaginationResponse<ProductResponse>(parameters.PageIndex , parameters.PageSize , count, result);
    }
    public async Task<ProductResponse> GetProductByIdAsync(int id)
    {
        var spec = new ProductsWithBrandAndTypeSpecifications(id);

        var product = await _unitofwork.GetRepositoryAsync<int, Product>().GetAsync(spec);

        if (product is null) throw new ProductNotFoundExeptoin(id); //404

        var result = _mapper.Map<ProductResponse>(product);
        return result;
    }
    public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
    {
        var spec = new BaseSpecifications<int, ProductBrand>(null);

        var brands = await _unitofwork.GetRepositoryAsync<int, ProductBrand>().GetAllAsync(spec);
        var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
        return result;
    } 
    public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
    {
        var spec = new BaseSpecifications<int, ProductType>(null);

        var types = await _unitofwork.GetRepositoryAsync<int, ProductType>().GetAllAsync(spec);
        var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
        return result;
    }

   
}
