using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.QueryModels
{
    public class ProductQueryData
    {
        private int? _take;
        private int? _pageIndex;

        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public SortingBase? sortingBase { get; set; }
        public string? searchName { get; set; }

        public int? Take { 
            get { return _take; } 
            set { 
                if (value.HasValue && value.Value > 0 && value < 10) 
                    _take = value; 
                else 
                    _take = 10;
            }
        } 
        public int? PageIndex { 
            get { return _pageIndex; } 
            set { 
                if (value.HasValue && value.Value >= 0) 
                    _pageIndex = value; 
                else 
                    _pageIndex = 0; 
            }
        }
    }

    
    public enum SortingBase
    {
        PriceAsc = 1,
        PriceDesc = 2,
        NameAsc = 3,
        NameDesc = 4
    }
}
