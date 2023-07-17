using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Common.ValueObjects
{
    public class Money
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}
