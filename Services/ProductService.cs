using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using ServiceAbstraction;
using Services.Exceptions_Implementation;
using Services.Specification_Implementation;
using Shared.DatatoObject_Dtos_;
using Shared.PaginatedModel;
using Shared.QueryModels;

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

        public async Task<GeneralPaginatedModel<ProductDto>> GetAllProductsAsync(ProductQueryData productQueryData)
        {
            var pRepo = _unitOfWork.GetRepository<Product, int>();
            var products = await pRepo.GetAllAsync(new ProductTypeBrandSpecification(productQueryData)); // Consider using async/await properly
            var ProductsDto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            ProductCountSpecification productCountSpecification = new ProductCountSpecification(productQueryData);
            var totalCount = await pRepo.CountAsync(productCountSpecification); // Consider using async/await properly

            GeneralPaginatedModel<ProductDto> paginatedProducts = new GeneralPaginatedModel<ProductDto>(productQueryData.PageIndex.GetValueOrDefault(),
                ProductsDto.Count(), totalCount)
            {
                Items = ProductsDto
            };
            return paginatedProducts;
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var pRepo = _unitOfWork.GetRepository<Product, int>();
            var product = await pRepo.GetByIdAsync(new ProductTypeBrandSpecification(id)); // Consider using async/await properly
            var ProductDto = mapper.Map<Product, ProductDto>(product);
            if (ProductDto == null)
            {
                throw new NoProductFoundException(id); // Custom exception for not found product
            }
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
