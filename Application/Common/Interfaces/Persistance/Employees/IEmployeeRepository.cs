﻿using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistance.Employees
{
    public interface IEmployeeRepository : IEntityRepository<Employee>
    {
    }
}
