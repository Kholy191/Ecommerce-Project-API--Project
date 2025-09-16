

using Domain.Exceptions;

namespace Services.Exceptions_Implementation
{
    sealed public class AddressNotFoundException : NotFoundException
    {
        public AddressNotFoundException() : base("There is No Address") { }
    }
}
