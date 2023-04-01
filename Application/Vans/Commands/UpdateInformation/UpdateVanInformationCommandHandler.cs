using Application.Common.Interfaces.Persistence;
using Application.Vans.Errors;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Vans.Commands.UpdateInformation
{
    public class UpdateVanInformationCommandHandler : IRequestHandler<UpdateVanInformationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVanInformationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateVanInformationCommand request, CancellationToken cancellationToken)
        {
            Van? van = await _unitOfWork.Vans.GetFirstOrDefaultAsync(v => v.Id == request.Id && v.CompanyId == request.CompanyId, cancellationToken: cancellationToken);

            if (van == null)
                return Result.Fail(new VanDoesntBelongToCompanyOrDoenstExist());

            SetupVan(request, van);

            _unitOfWork.Vans.Update(van);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();

        }

        private static void SetupVan(UpdateVanInformationCommand request, Van van)
        {
            van.Manufacturer = request.Manufacturer;
            van.Model = request.Model;
            van.DateOfManufacturing = request.DateOfManufacturing;
            van.Dimensions = request.Dimensions;
            van.Capacity = request.Capacity;
        }
    }
}

