using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.QueryModels;

namespace Services.Specification_Implementation
{
    public class ProductCountSpecification : Specification<Domain.Entities.Product, int>
    {
        public ProductCountSpecification(ProductQueryData ProductQueryData) :
            base(p =>
            (!ProductQueryData.BrandId.HasValue || p.BrandId == ProductQueryData.BrandId.Value) &&
            (!ProductQueryData.TypeId.HasValue || p.TypeId == ProductQueryData.TypeId.Value)
            && (string.IsNullOrWhiteSpace(ProductQueryData.searchName) || p.Name.ToLower().Contains(ProductQueryData.searchName.ToLower())))
        {

        }
    }
}
