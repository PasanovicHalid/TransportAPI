using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace Application.Companies.Commands.Create
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company company = new(request.Name,
                                  new Address(request.Street,
                                              request.City,
                                              request.State,
                                              request.PostalCode,
                                              request.Country));

            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
