using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Services.Exceptions_Implementation
{
    sealed public class userNotFoundException(string? message) : NotFoundException($"User with Email {message} is not found")
    {
        
    }
}
