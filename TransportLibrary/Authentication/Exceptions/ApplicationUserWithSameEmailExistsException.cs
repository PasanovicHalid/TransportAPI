using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Shared.Exceptions;

namespace TransportLibrary.Authentication.Exceptions
{
    public class ApplicationUserWithSameEmailExistsException : StatusException
    {
        public ApplicationUserWithSameEmailExistsException(string? message) : base(400, message)
        {
        }
    }
}
