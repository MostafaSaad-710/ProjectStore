using AutoMapper;
using Domaine.Contracts;
using Services.Abstraction;
using Services.Abstraction.Products;
using Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitofwork _unitofwork, IMapper _mapper) : IServiceManager
    {
        public IProductServices productServices { get; } = new ProductServices(_unitofwork , _mapper);
    }
}
