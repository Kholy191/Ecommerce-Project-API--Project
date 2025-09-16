using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.BasketEntites;
using Domain.Entities.IdentityEntities;
using Domain.Entities.OrderEntities;
using Shared.AddressDtos;
using Shared.DatatoObject_Dtos_.BasketDtos;

namespace Services.AutoMapperProfile
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
        }
    }
}
