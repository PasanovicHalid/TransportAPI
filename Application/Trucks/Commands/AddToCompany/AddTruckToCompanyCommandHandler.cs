using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Trucks.Commands.AddToCompany
{
    public class AddTruckToCompanyCommandHandler : IRequestHandler<AddTruckToCompanyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTruckToCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddTruckToCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? company = await _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken: cancellationToken);

            if (company == null)
                return Result.Fail(new EntityDoesntExist(request.CompanyId, nameof(Company)));

            Truck truck = new Truck(request.Manufacturer, request.Model, request.DateOfManufacturing, request.Dimensions);

            company.Vehicles.Add(truck);

            _unitOfWork.Companies.Update(company);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

