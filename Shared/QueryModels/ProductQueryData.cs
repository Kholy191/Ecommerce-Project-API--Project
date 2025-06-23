using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.QueryModels
{
    public class ProductQueryData
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public SortingBase? sortingBase { get; set; }
        public string? searchName { get; set; }
    }
    public enum SortingBase
    {
        PriceAsc = 1,
        PriceDesc = 2,
        NameAsc = 3,
        NameDesc = 4
    }
}
