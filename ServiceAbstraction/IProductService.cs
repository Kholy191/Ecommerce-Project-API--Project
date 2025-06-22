using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DatatoObject_Dtos_;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        public Task<ProductDto> GetProductByIdAsync(int id);
        public Task<IEnumerable<ProductTypeDto>> GetAllProductTypesAsync();
        public Task<IEnumerable<BrandDto>> GetAllProductBrandsAsync();
    }
}
