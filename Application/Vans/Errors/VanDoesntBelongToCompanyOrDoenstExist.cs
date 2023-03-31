using Domain.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Vans.Errors
{
    public class VanDoesntBelongToCompanyOrDoenstExist : IStatusCodeError
    {
        public string Message => "Van doesn't belong to company or doesn't exist";

        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons => new();

        public Dictionary<string, object> Metadata => new();
    }
}