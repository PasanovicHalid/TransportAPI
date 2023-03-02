using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Authentication
{
    public class RegistrationRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

        [Required]
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
