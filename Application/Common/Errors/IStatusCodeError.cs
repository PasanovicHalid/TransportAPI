using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors
{
    public interface IStatusCodeError : IError
    {
        public HttpStatusCode Code { get; }
    }
}
