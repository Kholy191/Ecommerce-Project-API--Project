using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions_Implementation
{
    public class NotAuthorizedException(string Message = "Invalid Email or Password") : Exception(Message)
    {
    }
}
