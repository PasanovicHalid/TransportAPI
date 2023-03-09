using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors
{
    public class EntityDoesntExist : IStatusCodeError
    {
        private ulong _id;

        private string _name;

        private HttpStatusCode _statusCode = HttpStatusCode.NotFound;

        public EntityDoesntExist(ulong id, string name)
        {
            _id = id;
            _name = name;
        }

        public EntityDoesntExist(ulong id, string name, HttpStatusCode statusCode)
        {
            _id = id;
            _name = name;
            _statusCode = statusCode;
        }

        public HttpStatusCode Code => _statusCode;

        public List<IError> Reasons => new();

        public string Message => _name + "with Id:" + _id + " doesn't exist in database";

        public Dictionary<string, object> Metadata => new();
    }
}
