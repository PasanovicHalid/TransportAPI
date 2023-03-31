using Domain.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Employees.Errors
{
    public class AdminCantBeFired : IStatusCodeError
    {
        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons => new();

        public string Message => "Admin can't be fired";

        public Dictionary<string, object> Metadata => new();

    }
}