using Domain.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Common.Errors
{
    public class EntityDoesntExist : IStatusCodeError
    {
        private readonly ulong _id;

        private readonly string _name;

        private readonly HttpStatusCode _statusCode = HttpStatusCode.NotFound;

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

        public string Message => _name + " with Id:" + _id + " doesn't exist in database";

        public Dictionary<string, object> Metadata => new();
    }
}
