using Domain.Common.Errors;
using FluentResults;
using System.Net;

namespace Application.Employees.Errors
{
    public class EmployeeIsntWorkingForAdminOrDoesntExist : IStatusCodeError
    {
        public HttpStatusCode Code => HttpStatusCode.BadRequest;

        public List<IError> Reasons => new();

        public string Message => "Employee isn't working for admin or doesnt exist";

        public Dictionary<string, object> Metadata => new();
    }
}