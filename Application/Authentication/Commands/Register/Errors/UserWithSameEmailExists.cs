using Domain.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Authentication.Commands.Register.Errors
{
    public class UserWithSameEmailExists : IStatusCodeError
    {
        public List<IError> Reasons { get; } = new();

        public string Message { get; } = "User with same email already exists";

        public Dictionary<string, object> Metadata { get; } = new();

        public HttpStatusCode Code { get; } = HttpStatusCode.Conflict;
    }
}
