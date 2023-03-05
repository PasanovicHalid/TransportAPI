using Application.Authentication.Commands.Register.Admin;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Authentication.Adapters
{
    public class AdminRegistrationAdapter : Profile
    {
        public AdminRegistrationAdapter()
        {
            CreateMap<AdminRegistrationRequest, RegisterAdminCommand>();
        }
    }
}
