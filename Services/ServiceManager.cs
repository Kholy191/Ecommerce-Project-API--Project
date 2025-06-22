using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using ServiceAbstraction;

namespace Services
{
    public class ServiceManager(IMapper mapper , IUnitOfWork unitOfWork) : IServiceManager
    {
        Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _productService.Value;
    }
}
