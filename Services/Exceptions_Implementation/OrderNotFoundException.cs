using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Services.Exceptions_Implementation
{
    internal class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid id) : base($"No order found with ID: {id}.")
        {
        }

    }
}
