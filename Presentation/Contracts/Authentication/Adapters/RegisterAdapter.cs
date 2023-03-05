using Application.Authentication.Commands.Register.SuperAdmin;
using Application.Authentication.Queries.Login;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Authentication.Adapters
{
    public class RegisterAdapter : Profile
    {
        public RegisterAdapter()
        {
            CreateMap<RegistrationRequest, RegisterSuperAdminCommand>();
        }
    }
}
