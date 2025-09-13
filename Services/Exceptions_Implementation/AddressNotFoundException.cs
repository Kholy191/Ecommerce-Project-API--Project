

namespace Services.Exceptions_Implementation
{
    sealed public class AddressNotFoundException : Exception
    {
        public AddressNotFoundException() : base("There is No Address") { }
    }
}
