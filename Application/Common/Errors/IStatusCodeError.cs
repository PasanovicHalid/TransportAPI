using FluentResults;
using System.Net;

namespace Application.Common.Errors
{
    public interface IStatusCodeError : IError
    {
        public HttpStatusCode Code { get; }
    }
}
