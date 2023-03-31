using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Companies.Queries.FindById
{
    public class FindCompanyByIdQueryHandler : IRequestHandler<FindCompanyByIdQuery, Result<Company>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FindCompanyByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Company>> Handle(FindCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            Company? company = await _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken);

            if (company == null)
                return Result.Fail(new EntityDoesntExist(request.Id, nameof(Company)));

            return company;
        }
    }
}
