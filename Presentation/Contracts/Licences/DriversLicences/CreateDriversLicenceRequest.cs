using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Licences.DriversLicences
{
    public class CreateDriversLicenceRequest
    {
        public string Category { get; set; }

        public DateTime IssuingDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ulong DriverId { get; set; }
    }
}
