using Application.Common.Interfaces.Persistance;
using Application.Companies.Errors;
using Domain;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Commands.Delete
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? company = _unitOfWork.Companies.GetFirstOrDefault(c => c.Id == request.Id);

            if(company == null)
                return Result.Fail(new CompanyDoesntExist(request.Id));

            _unitOfWork.Companies.Remove(company);
            _unitOfWork.Save();

            return Result.Ok();
        }
    }
}
