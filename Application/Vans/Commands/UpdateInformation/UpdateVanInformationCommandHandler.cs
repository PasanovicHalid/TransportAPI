using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Vans.Errors;

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
            Van? van = await _unitOfWork.Vans.GetFirstOrDefaultAsync(v => v.Id == request.Id && v.OwnedBy.Employees.Any(e => e.IdentityId == request.AdminIdentityId));

            if (van == null)
                return Result.Fail(new VanDoesntBelongToCompanyOrDoenstExist());

            SetupVan(request, van);

            _unitOfWork.Vans.Update(van);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();

        }

        private static void SetupVan(UpdateVanInformationCommand request, Van? van)
        {
            van.Manufacturer = request.Manufacturer;
            van.Model = request.Model;
            van.DateOfManufacturing = request.DateOfManufacturing;
            van.Dimensions = request.Dimensions;
            van.Capacity = request.Capacity;
        }
    }
}

