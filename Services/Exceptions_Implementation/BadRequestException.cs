using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions_Implementation
{
    sealed public class BadRequestException(IEnumerable<string> values) : Exception("Validation Failed")
    {
        public IEnumerable<string> Errors { get; set; } = values;
    }
}
