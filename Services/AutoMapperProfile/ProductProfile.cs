using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Services.AutoMapperProfile
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile() 
        {
            CreateMap<Domain.Entities.Product, Shared.DatatoObject_Dtos_.ProductDto>()
                .ForMember(dest => dest.ProductBrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());

            CreateMap<Domain.Entities.ProductBrand, Shared.DatatoObject_Dtos_.BrandDto>();
            CreateMap<Domain.Entities.ProductType, Shared.DatatoObject_Dtos_.ProductTypeDto>();
        }
    }
}
