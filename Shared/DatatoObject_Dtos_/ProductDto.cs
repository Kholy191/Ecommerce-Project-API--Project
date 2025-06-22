using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DatatoObject_Dtos_
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int TypeId { get; set; }
        public int BrandId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductBrandName { get; set; }
    }
}
