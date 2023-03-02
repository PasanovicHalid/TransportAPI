using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    public class ValidationError : IError
    {
        public List<IError> Reasons => new();

        public string Message => "Validation failed!";

        public Dictionary<string, object> Metadata => new();
    }
}
