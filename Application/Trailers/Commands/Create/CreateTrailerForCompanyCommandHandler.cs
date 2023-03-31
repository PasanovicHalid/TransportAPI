using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Trailers.Commands.Create
{
    public class CreateTrailerForCompanyCommandHandler : IRequestHandler<CreateTrailerForCompanyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTrailerForCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateTrailerForCompanyCommand request, CancellationToken cancellationToken)
        {
            Company? company = await _unitOfWork.Companies.GetFirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken: cancellationToken);

            if (company == null)
                return Result.Fail(new EntityDoesntExist(request.CompanyId, nameof(Company)));

            Trailer trailer = new Trailer(request.Capacity, request.CompanyId);

            await _unitOfWork.Trailers.AddAsync(trailer, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

