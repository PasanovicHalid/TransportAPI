using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Shared;

namespace TestLibrary.Users.Exceptions
{

    public class FailedLoginException : StatusException
    {
        public FailedLoginException(int status, string? message) : base(status, message)
        {
        }
    }
}
