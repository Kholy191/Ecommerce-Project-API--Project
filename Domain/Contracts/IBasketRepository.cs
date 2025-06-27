using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.BasketEntites;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        public Task<Basket?> CreateorUpdateBasketAsync(Basket basket, TimeSpan time);
        public Task<Basket?> GetBasketAsync(string id);
        public Task<bool> DeleteBasketAsync(string id);
    }
}
