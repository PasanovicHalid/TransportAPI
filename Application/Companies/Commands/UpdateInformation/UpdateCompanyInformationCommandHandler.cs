using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace Application.Companies.Commands.UpdateInformation
{
    public class UpdateCompanyInformationCommandHandler : IRequestHandler<UpdateCompanyInformationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCompanyInformationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCompanyInformationCommand request, CancellationToken cancellationToken)
        {
            Company? companyFromDb = await _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken);

            if (companyFromDb == null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Company)));

            SetupUpdatedCompany(request, companyFromDb);

            _unitOfWork.Companies.Update(companyFromDb);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }

        private static void SetupUpdatedCompany(UpdateCompanyInformationCommand request, Company companyFromDb)
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
