using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.BasketEntites;
using Shared.DatatoObject_Dtos_.BasketDtos;

namespace Services.AutoMapperProfile
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap(); 
        }
    }
}
