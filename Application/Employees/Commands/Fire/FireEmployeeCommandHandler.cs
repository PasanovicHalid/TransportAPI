using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Commands.Fire
{
    public class FireEmployeeCommandHandler : IRequestHandler<FireEmployeeCommand, Result>
    {
        public async Task<Result> Handle(FireEmployeeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}



