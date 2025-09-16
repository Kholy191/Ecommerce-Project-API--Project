using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.AddressDtos;

namespace Shared.OrderDtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryMethodName { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
