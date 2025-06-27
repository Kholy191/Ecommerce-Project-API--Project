using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketEntites;
using ServiceAbstraction;
using Services.Exceptions_Implementation;
using Shared.DatatoObject_Dtos_.BasketDtos;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateorUpdateBasket(BasketDto basket)
        {
            var UpdatedBasket = _mapper.Map<BasketDto,Basket>(basket);
            UpdatedBasket = await basketRepository.CreateorUpdateBasketAsync(UpdatedBasket, TimeSpan.FromDays(30));
            if (UpdatedBasket != null)
            {
                return await GetBasket(basket.Id);
            }
            else
            {
                throw new Exception("Can't Create or Update the Basket");
            }
        }

        public async Task<bool> DeleteBasket(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            if (basket is null)
            {
                throw new NoBasketFoundException(id);
            }
            else
            {
                return await basketRepository.DeleteBasketAsync(id);
            }
        }

        public async Task<BasketDto> GetBasket(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            if (basket is null)
            {
                throw new NoBasketFoundException(id);
            }
            else
            {
                return _mapper.Map<Basket,BasketDto>(basket);
            }
        }
    }
}
