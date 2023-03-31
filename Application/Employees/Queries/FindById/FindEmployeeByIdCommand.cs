using MediatR;
using FluentResults;
using FluentValidation;

namespace Application.Employees.Queries.FindById
{
    public class FindEmployeeByIdCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
    }

    public class FindEmployeeByIdValidator : AbstractValidator<FindEmployeeByIdCommand>
    {
        public FindEmployeeByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}