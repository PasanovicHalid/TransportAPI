using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportLibrary.Authentication.Model
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string name) : base(name)
        {
        }
    }
}
