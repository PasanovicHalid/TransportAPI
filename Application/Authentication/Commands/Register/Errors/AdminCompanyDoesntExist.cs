using Domain.Common.Errors;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Register.Errors
{
    internal class AdminCompanyDoesntExist : IStatusCodeError
    {
        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons => new();

        public string Message => "Admins company doesnt exist in DB";

        public Dictionary<string, object> Metadata => new();
    }
}
