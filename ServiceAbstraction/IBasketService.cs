using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DatatoObject_Dtos_.BasketDtos;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        public Task<BasketDto> GetBasket(string id);
        public Task<BasketDto> CreateorUpdateBasket(BasketDto basket);
        public Task<bool> DeleteBasket(string id);
    }
}
