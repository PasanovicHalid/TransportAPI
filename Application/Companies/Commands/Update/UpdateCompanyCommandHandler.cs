using Application.Common.Errors;
using Application.Common.Interfaces.Persistance;
using Domain.Companies;
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

            companyFromDb.Name = request.Name;

            _unitOfWork.Companies.Update(companyFromDb);
            _unitOfWork.Save();

            return Result.Ok();
        }
    }
}
