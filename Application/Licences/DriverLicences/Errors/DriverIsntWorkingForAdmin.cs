using Application.Common.Errors;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Licences.DriverLicences.Errors
{
    public class DriverIsntWorkingForAdmin : IStatusCodeError
    {
        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons => new();

        public string Message => "Driver doesn't work for Admin";

        public Dictionary<string, object> Metadata => new();
    }
}
