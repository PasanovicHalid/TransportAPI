using Domain.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Authentication.Queries.Login.Errors
{
    internal class LoginFailed : IStatusCodeError
    {
        public List<IError> Reasons { get; } = new();

        public string Message { get; } = "Failed Login!";

        public Dictionary<string, object> Metadata { get; } = new();

        public HttpStatusCode Code { get; } = HttpStatusCode.NotFound;
    }
}
