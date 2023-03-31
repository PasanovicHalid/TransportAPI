using Domain.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Drivers.Errors
{
    public class DriverIsntWorkingForAdminOrDoesntExist : IStatusCodeError
    {
        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons => new();

        public string Message => "Driver doesn't work for Admin or doesn't exist";

        public Dictionary<string, object> Metadata => new();
    }
}
