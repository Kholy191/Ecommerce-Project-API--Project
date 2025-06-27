using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.PaginatedModel
{
    public class GeneralPaginatedModel<TEntity> where TEntity : class
    {
        public GeneralPaginatedModel(int pageIndex, int pageSize, int totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public int PageIndex { get; set; } = 1; // Default to the first page
        public int PageSize { get; set; } = 10; // Default to 10 items per page
        public int TotalCount { get; set; } // Total number of items across all pages

        public IEnumerable<TEntity> Items { get; set; } = new List<TEntity>();

    }
}
