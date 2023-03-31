using MediatR;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Commands.UpdateInformationByIdentity
{
    public class UpdateEmployeeInformationByIdentityCommandHandler : IRequestHandler<UpdateEmployeeInformationByIdentityCommand, Result>
    {
        public async Task<Result> Handle(UpdateEmployeeInformationByIdentityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}