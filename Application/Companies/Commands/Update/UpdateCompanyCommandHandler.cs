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

namespace Application.Companies.Commands.Update
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? companyFromDb = _unitOfWork.Companies.GetFirstOrDefault(c => c.Id == request.Id);

            if (companyFromDb == null)
                return Result.Fail(new CompanyDoesntExist(request.Id));

            companyFromDb.Name = request.Name;

            _unitOfWork.Companies.Update(companyFromDb);
            _unitOfWork.Save();

            return Result.Ok();
        }
    }
}
