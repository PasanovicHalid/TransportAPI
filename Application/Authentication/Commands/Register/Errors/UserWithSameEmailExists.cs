using Application.Common.Errors;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
