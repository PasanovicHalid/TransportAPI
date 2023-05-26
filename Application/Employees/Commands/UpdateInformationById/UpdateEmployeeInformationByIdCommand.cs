using Domain.ValueObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Employees.Commands.UpdateInformationById
{
    public class UpdateEmployeeInformationByIdCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public Address Address { get; set; }
        public ulong CompanyId { get; set; }
    }

    public class UpdateEmployeeInformationByIdValidator : AbstractValidator<UpdateEmployeeInformationByIdCommand>
    {
        public UpdateEmployeeInformationByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Salary).NotEmpty()
                                  .GreaterThan(0);

            RuleFor(x => x.Address).NotNull();

            RuleFor(x => x.Address.Street).NotEmpty();

            RuleFor(x => x.Address.City).NotEmpty();

            RuleFor(x => x.Address.State).NotEmpty();

            RuleFor(x => x.Address.PostalCode).NotEmpty();

            RuleFor(x => x.Address.Country).NotEmpty();

            RuleFor(x => x.CompanyId).NotEmpty();

            RuleFor(x => x.Address.GpsCoordinate!.Latitude)
                .NotNull()
                .When(x => x.Address.GpsCoordinate is not null);
            RuleFor(x => x.Address.GpsCoordinate!.Longitude)
                .NotNull()
                .When(x => x.Address.GpsCoordinate is not null);
        }
    }
}

