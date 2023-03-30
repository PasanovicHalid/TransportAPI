using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Companies.Commands.UpdateInformation
{
    public class UpdateCompanyInformationCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }

    public class UpdateCompanyInformationValidator : AbstractValidator<UpdateCompanyInformationCommand>
    {
        public UpdateCompanyInformationValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.State).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
        }
    }


}
