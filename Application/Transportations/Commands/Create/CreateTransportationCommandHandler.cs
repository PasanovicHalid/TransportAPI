using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Transportations.Commands.Create
{
    public class CreateTransportationCommandHandler : IRequestHandler<CreateTransportationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransportationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateTransportationCommand request, CancellationToken cancellationToken)
        {
            Company? company = _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == request.CompanyId).Result;

            if (company is null)
                return Result.Fail(new EntityDoesntExist(request.CompanyId, nameof(Company)));


            Transportation transportation = new Transportation(request.DateOfDeparture,
                                                               request.DateOfArrival,
                                                               request.Cargo,
                                                               request.Stops,
                                                               request.Received,
                                                               request.CompanyId);

            await _unitOfWork.Transportations.AddAsync(transportation, cancellationToken);

            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();

        }
    }
}
