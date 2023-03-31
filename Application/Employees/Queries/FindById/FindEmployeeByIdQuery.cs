using MediatR;
using FluentResults;
using FluentValidation;
using Domain.Entities;

namespace Application.Employees.Queries.FindById
{
    public class FindEmployeeByIdQuery : IRequest<Result<Employee>>
    {
        public ulong Id { get; set; }

        public string AdminIdentityId { get; set; }
    }

    public class FindEmployeeByIdValidator : AbstractValidator<FindEmployeeByIdQuery>
    {
        public FindEmployeeByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}