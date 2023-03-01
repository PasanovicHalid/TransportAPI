using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Register.Exceptions
{
    public class UserWithSameEmailExists : IError
    {
        public List<IError> Reasons => new();

        public string Message => "User with same email already exists";

        public Dictionary<string, object> Metadata => new();
    }
}
