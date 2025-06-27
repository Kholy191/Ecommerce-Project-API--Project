using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProductEntities;
using Microsoft.Extensions.Configuration;
using Shared.DatatoObject_Dtos_.ProductDtos;

namespace Services.AutoMapperProfile
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, ProductTypeDto>();
        }
    }
}
