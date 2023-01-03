using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASSOne.Platform.Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationPipelineBehavior<TRequest, TResponse>> _logger;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) => memberInfo.Name;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var typeName = request.GetType();

            _logger.LogDebug("Validating command {CommandType}", typeName);

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                _logger.LogWarning(
                    "Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName,
                    request, failures);

                var failureDetails = failures.Select(f => new
                {
                    f.ErrorMessage,
                    f.AttemptedValue
                });

                //TODO Uncomment throw new ValidationWithErrorException(Errors.General.ApplicationValidationError(failureDetails), failures);
                throw new Exception();
            }

            return await next();
        }
    }
}
