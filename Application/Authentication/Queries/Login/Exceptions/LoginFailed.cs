using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.Login.Exceptions
{
    internal class LoginFailed : IError
    {
        public List<IError> Reasons => new();

        public string Message => "Failed Login!";

        public Dictionary<string, object> Metadata => new();
    }
}
