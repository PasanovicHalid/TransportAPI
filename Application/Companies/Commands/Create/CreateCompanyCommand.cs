using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Companies.Commands.Create
{
    public class CreateCompanyCommand : IRequest<Result>
    {
        public string Name { get; set; } = null!;
    }

    public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
