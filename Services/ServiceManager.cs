using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;

namespace Services
{
    public class ServiceManager(IMapper mapper, IUnitOfWork unitOfWork, 
        IBasketRepository basketRepository, UserManager<AppUser> _useManger, 
        IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IServiceManager
    {
        Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _productService.Value;

        Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        public IBasketService BasketService => _basketService.Value;

        Lazy<IAuthenticationServices> _authenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationService(_useManger, configuration, mapper, httpContextAccessor));
        public IAuthenticationServices AuthenticationServices => _authenticationServices.Value;

        Lazy<IOrderServices> _orderServices = new Lazy<IOrderServices>(() => new OrderServices(unitOfWork , mapper, basketRepository));
        public IOrderServices OrderServices => _orderServices.Value;
    }
}
