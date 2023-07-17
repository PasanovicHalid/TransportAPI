using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Trucks.Commands.Delete
{
    public class DeleteTruckCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class DeleteTruckValidator : AbstractValidator<DeleteTruckCommand>
    {
        public DeleteTruckValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}
