using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Queries.FindById
{
    public class FindEmployeeByIdCommandHandler : IRequestHandler<FindEmployeeByIdCommand, Result>
    {
        public async Task<Result> Handle(FindEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}