using Domain.Companies;
using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Queries.FindCompanyById
{
    public class FindCompanyByIdCommand : IRequest<Result<Company>>
    {
        public ulong Id { get; set; }

        public FindCompanyByIdCommand()
        {
        }

        public FindCompanyByIdCommand(ulong id)
        {
            Id = id;
        }
    }

    public class FindCompanyByIdValidator : AbstractValidator<FindCompanyByIdCommand>
    {
        public FindCompanyByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
