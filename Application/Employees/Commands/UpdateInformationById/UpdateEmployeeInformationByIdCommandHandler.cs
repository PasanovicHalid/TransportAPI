using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Commands.UpdateInformationById
{
    public class UpdateEmployeeInformationByIdCommandHandler : IRequestHandler<UpdateEmployeeInformationByIdCommand, Result>
    {
        public async Task<Result> Handle(UpdateEmployeeInformationByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
