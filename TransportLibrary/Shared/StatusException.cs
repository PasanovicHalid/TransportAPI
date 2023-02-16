using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportLibrary.Shared
{
    public abstract class StatusException : Exception
    {
        public StatusException(int status, string? message) : base(message)
        {
            Status = status;
        }

        public int Status { get; set; }
    }
}
