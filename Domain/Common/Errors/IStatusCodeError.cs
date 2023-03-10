using FluentResults;
using System.Net;

namespace Domain.Common.Errors
{
    public interface IStatusCodeError : IError
    {
        public HttpStatusCode Code { get; }
    }
}
