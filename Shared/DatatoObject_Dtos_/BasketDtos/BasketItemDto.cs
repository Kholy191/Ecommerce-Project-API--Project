using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DatatoObject_Dtos_.BasketDtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        [Range(1, Double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1, 100, ErrorMessage = "Quantity must between 1 to 100.")]
        public int Quantity { get; set; }
    }
}
