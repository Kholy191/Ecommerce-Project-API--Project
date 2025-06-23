using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities;

namespace Services.Specification_Implementation
{
    internal class ProductTypeBrandSpecification : Specification<Product, int>
    {
        public ProductTypeBrandSpecification(int? TypeId, int? BrandId) : 
            base(p => 
            (!BrandId.HasValue || p.BrandId == BrandId.Value) &&
            (!TypeId.HasValue || p.TypeId == TypeId.Value))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
        public ProductTypeBrandSpecification(int Id) : base(P=>P.Id == Id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
