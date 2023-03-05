using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Authentication
{
    public class AdminRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public double Salary { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public ulong CompanyId { get; set; }
    }
}
