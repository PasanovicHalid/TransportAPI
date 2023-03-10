using Application.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Common.Behaviors
{
    public class ValidationError : IStatusCodeError
    {
        public List<IError> Reasons { get; } = new();

        public string Message { get; } = "Validation failed!";

        public Dictionary<string, object> Metadata { get; } = new();

        public HttpStatusCode Code { get; } = HttpStatusCode.BadRequest;
    }
}
