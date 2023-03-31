using MediatR;
using FluentResults;
using FluentValidation;

namespace Application.Employees.Commands.Fire
{
    public class FireEmployeeCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
    }

    public class FireEmployeeValidator : AbstractValidator<FireEmployeeCommand>
    {
        public FireEmployeeValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

