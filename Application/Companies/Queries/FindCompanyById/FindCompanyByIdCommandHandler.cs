using Application.Common.Interfaces.Persistance;
using Application.Companies.Errors;
using Domain.Companies;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Queries.FindCompanyById
{
    public class FindCompanyByIdCommandHandler : IRequestHandler<FindCompanyByIdCommand, Result<Company>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FindCompanyByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Company>> Handle(FindCompanyByIdCommand request, CancellationToken cancellationToken)
        {
            Company? company = _unitOfWork.Companies.GetFirstOrDefault(c => c.Id == request.Id);

            if (company == null)
                return Result.Fail(new CompanyDoesntExist(request.Id));

            return company;
        }
    }
}
