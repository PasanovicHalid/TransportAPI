using Application.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Licences.DriverLicences.Errors
{
    public class EmployeeIsntDriver : IStatusCodeError
    {
        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons => new();

        public string Message => "Employee isn't a Driver";

        public Dictionary<string, object> Metadata => new();
    }
}
