using MediatR;
using FluentResults;
using FluentValidation;
using Domain.ValueObjects;

namespace Application.Employees.Commands.UpdateInformationByIdentity
{
    public class UpdateEmployeeInformationByIdentityCommand : IRequest<Result>
    {
        public string IdentityId { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public Address Address { get; set; }
    }

    public class UpdateEmployeeInformationByIdentityValidator : AbstractValidator<UpdateEmployeeInformationByIdentityCommand>
    {
        public UpdateEmployeeInformationByIdentityValidator()
        {
            RuleFor(x => x.IdentityId).NotEmpty();

            RuleFor(x => x.Role).NotEmpty();

            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.MiddleName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Salary).NotEmpty()
                                  .GreaterThan(0);

            RuleFor(x => x.Address).NotEmpty();
        }
    }
}

