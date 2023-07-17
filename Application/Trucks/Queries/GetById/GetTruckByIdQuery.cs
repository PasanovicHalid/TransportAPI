using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Trucks.Queries.GetById
{
    public class GetTruckByIdQuery : IRequest<Result<Truck>>
    {
        public ulong Id { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class GetTruckByIdValidator : AbstractValidator<GetTruckByIdQuery>
    {
        public GetTruckByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}
