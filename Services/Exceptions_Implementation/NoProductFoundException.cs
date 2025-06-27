using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Services.Exceptions_Implementation
{
    public class NoProductFoundException : NotFoundException
    {
        public NoProductFoundException(int id) : base($"No product found with ID: {id}.")
        {
        }
    }
}
