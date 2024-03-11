using Template.Modules.Shared.Core.Exceptions;
using Template.Modules.Shared.Core.Extensions;
using FluentValidation;
using MediatR;

namespace Template.Modules.Shared.Infrastructure.Middleware
{
    public class ValidationBehaviourMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviourMiddleware(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (!failures.Any()) return await next();

            List<ErrorObject> errors = new();

            foreach (var ex in failures)
            {
                errors.Add(new ErrorObject()
                {
                    Code = ex.ErrorCode.ToSnakeCase(),
                    Message = ex.ErrorMessage,
                    FieldName = ex.PropertyName
                });
            }

            throw new ValidatorException(errors);
        }
    }
}
