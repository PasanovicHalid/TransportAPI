using Application.Common.Interfaces.Persistance;
using Domain.Companies;
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
            Company company = new Company(request.Name);

            _unitOfWork.Companies.Add(company);
            _unitOfWork.Save();

            return Result.Ok();
        }
    }
}
