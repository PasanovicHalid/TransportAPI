using Application.Drivers.Queries.GetPerformanceDataOfDriver;
using Domain.PlainObjects;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Drivers.Queries.GetPerformanceOfDriver
{
    public class GetPerformanceDataOfDriverQuery : IRequest<Result<DriverPerformanceData>>
    {
        public ulong DriverId { get; set; }
        public ulong CompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetPerformanceDataOfDriverValidator : AbstractValidator<GetPerformanceDataOfDriverQuery>
    {
        public GetPerformanceDataOfDriverValidator()
        {
            RuleFor(x => x.DriverId).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(y => y.StartDate).NotEmpty();
            RuleFor(z => z.EndDate).NotEmpty();
        }
    }   
}