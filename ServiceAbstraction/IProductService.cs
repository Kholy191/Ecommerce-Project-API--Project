using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DatatoObject_Dtos_;
using Shared.QueryModels;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductQueryData productQueryData);
        public Task<ProductDto> GetProductByIdAsync(int id);
        public Task<IEnumerable<ProductTypeDto>> GetAllProductTypesAsync();
        public Task<IEnumerable<BrandDto>> GetAllProductBrandsAsync();
    }
}
