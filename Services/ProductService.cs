using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using ServiceAbstraction;
using Shared.DatatoObject_Dtos_;

namespace Services
{
    public class ProductService : IProductService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper _mapper)
        {
            _unitOfWork = unitOfWork;
            mapper = _mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var pRepo = _unitOfWork.GetRepository<Product, int>();
            var products = await pRepo.GetAllAsync(); // Consider using async/await properly
            var ProductsDto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return ProductsDto;
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var pRepo = _unitOfWork.GetRepository<Product, int>();
            var product = await pRepo.GetByIdAsync(id); // Consider using async/await properly
            var ProductDto = mapper.Map<Product, ProductDto>(product);
            return ProductDto;
        }
        public async Task<IEnumerable<BrandDto>> GetAllProductBrandsAsync()
        {
            var bRepo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await bRepo.GetAllAsync(); // Consider using async/await properly
            var brandsDto = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandsDto;
        }
        public async Task<IEnumerable<ProductTypeDto>> GetAllProductTypesAsync()
        {
            var TRepo = _unitOfWork.GetRepository<ProductType, int>();
            var Types = await TRepo.GetAllAsync(); // Consider using async/await properly
            var TypesDto = mapper.Map<IEnumerable<ProductType>, IEnumerable<ProductTypeDto>>(Types);
            return TypesDto;
        }
    }
}
