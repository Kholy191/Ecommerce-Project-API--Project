using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.AddressDtos;

namespace Shared.OrderDtos
{
    public class OrderCreateDto
    {
        public string BasketId { get; set; } = string.Empty;
        public AddressDto Address { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
