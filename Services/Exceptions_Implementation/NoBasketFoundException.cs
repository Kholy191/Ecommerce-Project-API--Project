using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Services.Exceptions_Implementation
{
    public class NoBasketFoundException(string Id) : NotFoundException($"There is no Basket with that name \"{Id}\"")
    {

    }
}
