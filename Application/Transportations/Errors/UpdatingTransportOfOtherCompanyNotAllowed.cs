using Domain.Common.Errors;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transportations.Errors
{
    internal class UpdatingTransportOfOtherCompanyNotAllowed : IStatusCodeError
    {
        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons => new();

        public string Message => "You cannot update a transport of another company";

        public Dictionary<string, object> Metadata => new();
    }
}
