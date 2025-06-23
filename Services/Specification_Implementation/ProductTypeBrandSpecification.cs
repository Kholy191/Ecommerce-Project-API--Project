using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities;
using Shared.QueryModels;

namespace Services.Specification_Implementation
{
    internal class ProductTypeBrandSpecification : Specification<Product, int>
    {
        public ProductTypeBrandSpecification(ProductQueryData ProductQueryData) : 
            base(p => 
            (!ProductQueryData.BrandId.HasValue || p.BrandId == ProductQueryData.BrandId.Value) &&
            (!ProductQueryData.TypeId.HasValue || p.TypeId == ProductQueryData.TypeId.Value) 
            && (string.IsNullOrWhiteSpace(ProductQueryData.searchName) || p.Name.ToLower().Contains(ProductQueryData.searchName.ToLower())))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            switch(ProductQueryData.sortingBase)
            {
                case SortingBase.PriceAsc:
                    SetOrderBy(p => p.Price);
                    break;
                case SortingBase.PriceDesc:
                    SetOrderByDescending(p => p.Price);
                    break;
                case SortingBase.NameAsc:
                    SetOrderBy(p => p.Name);
                    break;
                case SortingBase.NameDesc:
                    SetOrderByDescending(p => p.Name);
                    break;
                default:
                    break;
            }
        }
        public ProductTypeBrandSpecification(int Id) : base(P=>P.Id == Id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
