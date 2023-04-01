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

            RuleFor(x => x.MiddleName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Salary).NotEmpty()
                                  .GreaterThan(0);

            RuleFor(x => x.Address).NotEmpty();

            RuleFor(x => x.CompanyId).NotEmpty();
        }
    }
}

