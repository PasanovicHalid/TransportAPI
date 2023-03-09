﻿using Domain.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistance.Employees
{
    public interface IDriverRepository : IEntityRepository<Driver>
    {
    }
}
