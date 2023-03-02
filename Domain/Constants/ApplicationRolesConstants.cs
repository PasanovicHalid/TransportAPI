using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public static class ApplicationRolesConstants
    {
        public const string Admin = "Admin";
        public const string SuperAdmin = "SuperAdmin";
        public const string Driver = "Driver";

        //You have to add your new role here also
        public static readonly ReadOnlyCollection<string> Roles = new List<string>()
        {
            Admin,
            SuperAdmin,
            Driver
        }
        .AsReadOnly();

    }
}
