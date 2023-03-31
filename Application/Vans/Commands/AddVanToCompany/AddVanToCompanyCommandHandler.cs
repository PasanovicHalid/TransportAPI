using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Vans.Commands.AddVanToCompany
{
    public class AddVanToCompanyCommandHandler : IRequestHandler<AddVanToCompanyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddVanToCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddVanToCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? company = await _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken: cancellationToken);

            if (company == null)
                return Result.Fail(new EntityDoesntExist(request.CompanyId, nameof(Company)));

            Van van = new Van(request.Manufacturer, request.Model, request.DateOfManufacturing, request.Dimensions, request.Capacity);

            company.Vehicles.Add(van);

            _unitOfWork.Companies.Update(company);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

