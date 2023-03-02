using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ResultBase, new()
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null )
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request,
                                      RequestHandlerDelegate<TResponse> next,
                                      CancellationToken cancellationToken)
        {
            if( _validator is null)
                return await next();
            
            ValidationResult validatorResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.IsValid)
                return await next();

            var result = new TResponse();

            //Means that validation failed 400 is that code
            ValidationError validationErrors = new();

            List<Error> errors = validatorResult.Errors.ConvertAll(validationFailure => 
                new Error(validationFailure.PropertyName,
                          new Error(validationFailure.ErrorMessage)));

            validationErrors.Reasons.AddRange(errors);

            result.Reasons.Add(validationErrors);

            return result;
        }
    }
}
