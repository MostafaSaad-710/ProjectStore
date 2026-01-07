using AutoMapper;
using Domaine.Contracts;
using Domaine.Entities;
using Domaine.Entities.Products;
using Services.Abstraction.Products;
using Services.Specifications;
using Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products; // ركز الكود اللي بيرن ومتشاف بتاع البرودكت موديول انما الكود بتاع الفيتشر 

public class ProductServices(IUnitofwork _unitofwork , IMapper _mapper) : IProductServices
{ 
    public async Task<IEnumerable<ProductResponse>> GetAllProductAsync()
    {
        var spec = new BaseSpecifications<int , Product>(null);

        spec.Includes.Add(P => P.Brand);
        spec.Includes.Add(P => P.Type);

        var products = await _unitofwork.GetRepositoryAsync<int, Product>().GetAllAsync(spec); 
        var result = _mapper.Map<IEnumerable<ProductResponse>>(products);
        return result;
    }
    public async Task<ProductResponse> GetProductByIdAsync(int id)
    {
        var product = await _unitofwork.GetRepositoryAsync<int , Product>().GetAsync(id);
        var result = _mapper.Map<ProductResponse>(product);
        return result;
    }
    public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
    {
        var brands =  await _unitofwork.GetRepositoryAsync<int , ProductBrand>().GetAllAsync();
        var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
        return result;
    } 
    public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
    {
        var types = await _unitofwork.GetRepositoryAsync<int, ProductType>().GetAllAsync();
        var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
        return result;
    }

   
}
