﻿using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ResultBase, new()
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request,
                                      RequestHandlerDelegate<TResponse> next,
                                      CancellationToken cancellationToken)
        {
            if (_validator is null)
                return await next();

            ValidationResult validatorResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.IsValid)
                return await next();

            return SetupErrorResponse(validatorResult);
        }

        private static TResponse SetupErrorResponse(ValidationResult validatorResult)
        {
            var result = new TResponse();

            var validationErrors = new ValidationError();

            List<Error> errors = validatorResult.Errors.ConvertAll(validationFailure =>
                new Error(validationFailure.PropertyName,
                          new Error(validationFailure.ErrorMessage)));

            validationErrors.Reasons.AddRange(errors);

            result.Reasons.Add(validationErrors);

            return result;
        }
    }
}
