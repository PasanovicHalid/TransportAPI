using Application.Common.Errors;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Errors
{
    public class CompanyDoesntExist : IStatusCodeError
    {
        private ulong _id;
        public CompanyDoesntExist(ulong id)
        {
            _id = id;
        }

        public HttpStatusCode Code => HttpStatusCode.NotFound;

        public List<IError> Reasons => new();

        public string Message => "Company with Id:" + _id + " doesn't exist in database";

        public Dictionary<string, object> Metadata => new();
    }
}
