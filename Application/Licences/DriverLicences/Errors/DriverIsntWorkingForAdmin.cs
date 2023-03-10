using Application.Common.Errors;
using FluentResults;
using System.Net;

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
