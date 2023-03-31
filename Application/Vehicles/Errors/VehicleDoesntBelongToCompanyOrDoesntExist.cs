using Domain.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Vehicles.Errors
{
    public class VehicleDoesntBelongToCompanyOrDoesntExist : IStatusCodeError
    {
        public string Message => "Vehicle doesn't belong to company or doesn't exist";
        public HttpStatusCode Code => HttpStatusCode.BadRequest;
        public List<IError> Reasons => new();
        public Dictionary<string, object> Metadata => new();
    }
}