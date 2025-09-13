using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class ShippingAddress
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Street { get; set; }
        string City { get; set; }
        string Country { get; set; }
    }
}
