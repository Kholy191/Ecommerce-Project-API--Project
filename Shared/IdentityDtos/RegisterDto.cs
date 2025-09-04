using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shared.IdentityDtos
{
    public class RegisterDto
    {
        public string DisplayName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
