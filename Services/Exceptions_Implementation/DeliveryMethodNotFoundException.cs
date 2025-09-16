using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Services.Exceptions_Implementation
{
    sealed public class DeliveryMethodNotFoundException : NotFoundException
    {
        public DeliveryMethodNotFoundException() : base("There is No DeliveryMethod") { }

    }
}
