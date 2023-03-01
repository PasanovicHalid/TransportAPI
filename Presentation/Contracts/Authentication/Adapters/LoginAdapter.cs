﻿using Application.Authentication.Queries.Login;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Authentication.Adapters
{
    public class LoginAdapter : Profile
    {
        public LoginAdapter()
        {
            CreateMap<LoginRequest, LoginQuery>();
        }
    }
}
