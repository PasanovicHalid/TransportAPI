using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Shared;

namespace TransportLibrary.Authentication.Exceptions
{
    public class ApplicationUserDoesntExistException : StatusException
    {
        public ApplicationUserDoesntExistException(string? message) : base(404, message)
        {
        }
    }
}
