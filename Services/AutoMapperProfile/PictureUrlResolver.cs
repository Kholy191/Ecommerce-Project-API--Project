using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared.DatatoObject_Dtos_;

namespace Services.AutoMapperProfile
{
    internal class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        IConfiguration _configuration;
        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return null!; // or return a default image URL if you prefer
            }
            return $"{_configuration.GetSection("Urls")["URL1"]}/{source.PictureUrl}";
        }
    }
}
