using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Companies.Commands.Remove
{
    public class RemoveCompanyCommandHandler : IRequestHandler<RemoveCompanyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? company = _unitOfWork.Companies.GetFirstOrDefault(c => c.Id == request.Id);

            if (company == null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Company)));

            _unitOfWork.Companies.Remove(company);
            _unitOfWork.Save();

            return Result.Ok();
        }
    }
}
