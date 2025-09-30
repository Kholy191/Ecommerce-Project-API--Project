using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAbstraction;

namespace Services
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> productFactory , Func<IBasketService> basketFactory,
                                                   Func<IAuthenticationServices> authFactory, Func<IOrderServices> orderFactory) : IServiceManager
    {
        public IProductService ProductService => productFactory.Invoke();

        public IBasketService BasketService => basketFactory.Invoke();

        public IAuthenticationServices AuthenticationServices => authFactory.Invoke();

        public IOrderServices OrderServices => orderFactory.Invoke();
    }
}
