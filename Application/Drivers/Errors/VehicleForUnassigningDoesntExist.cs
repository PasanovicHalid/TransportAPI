using Domain.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Drivers.Errors
{
    public class VehicleForUnassigningDoesntExist : IStatusCodeError
    {
        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons => new();

        public string Message => "Driver doesnt have a vehicle for unasigning";

        public Dictionary<string, object> Metadata => new();
    }
}
