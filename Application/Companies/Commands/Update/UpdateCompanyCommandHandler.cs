using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using MediatR;

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
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Company)));

            SetupUpdatedCompany(request, companyFromDb);

            _unitOfWork.Companies.Update(companyFromDb);
            _unitOfWork.Save();

            return Result.Ok();
        }

        private static void SetupUpdatedCompany(UpdateCompanyCommand request, Company companyFromDb)
        {
            companyFromDb.Name = request.Name;
            companyFromDb.Address = new Address(request.Street,
                request.City,
                request.State,
                request.PostalCode,
                request.Country);
        }
    }
}
