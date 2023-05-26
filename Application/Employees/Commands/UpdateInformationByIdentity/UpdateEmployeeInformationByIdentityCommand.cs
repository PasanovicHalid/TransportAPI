using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Employees.Commands.UpdateInformationByIdentity
{
    public class UpdateEmployeeInformationByIdentityCommand : IRequest<Result>
    {
        public string IdentityId { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }

    public class UpdateEmployeeInformationByIdentityValidator : AbstractValidator<UpdateEmployeeInformationByIdentityCommand>
    {
        public UpdateEmployeeInformationByIdentityValidator()
        {
            RuleFor(x => x.IdentityId).NotEmpty();

            RuleFor(x => x.Role).NotEmpty();

            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Address).NotNull();

            RuleFor(x => x.Address.Street).NotEmpty();

            RuleFor(x => x.Address.City).NotEmpty();

            RuleFor(x => x.Address.State).NotEmpty();

            RuleFor(x => x.Address.PostalCode).NotEmpty();

            RuleFor(x => x.Address.Country).NotEmpty();

            RuleFor(x => x.Address.GpsCoordinate!.Latitude)
                .NotNull()
                .When(x => x.Address.GpsCoordinate is not null);
            RuleFor(x => x.Address.GpsCoordinate!.Longitude)
                .NotNull()
                .When(x => x.Address.GpsCoordinate is not null);
        }
    }
}

