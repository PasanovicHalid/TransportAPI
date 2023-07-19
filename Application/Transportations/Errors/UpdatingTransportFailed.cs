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
    internal class UpdatingTransportFailed : IStatusCodeError
    {
        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons => new();

        public string Message => "Updating basic transport info failed!";

        public Dictionary<string, object> Metadata => new();
    }
}
