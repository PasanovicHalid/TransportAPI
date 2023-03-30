using Domain.Common.Errors;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public class ObjectInInvalidState : IStatusCodeError
    {
        private readonly string _className;

        public ObjectInInvalidState(string className, List<IError> reasons)
        {
            _className = className;
            Reasons = reasons;
        }

        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons { get; private set; }

        public string Message => _className + " is in an invalid state";

        public Dictionary<string, object> Metadata => new();
    }
}
