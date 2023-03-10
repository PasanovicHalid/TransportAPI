﻿using Application.Common.Interfaces.Persistance.Employees;
using Infrastructure.Common.Persistance;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories.Employees
{
    public class UserRepository : Repository<IdentityUser>, IUserRepository
    {
        public UserRepository(TransportDbContext db) : base(db)
        {

        }
    }
}