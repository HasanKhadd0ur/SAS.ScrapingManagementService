using Ardalis.Result;
using FluentValidation;
using MediatR;


namespace SAS.ScrapingManagementService.Application.Behaviors.ValidationBehavior
{

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators = null)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators is null)
            {

                return await next();
            }

            var validationTasks = _validators.Select(v => v.ValidateAsync(request, cancellationToken));
            var validationResults = await Task.WhenAll(validationTasks);

            var errors = validationResults.SelectMany(r => r.Errors)
                                            .Where(e => e != null)
                                            .Select(e => new ValidationError(e.ErrorCode, e.ErrorMessage, e.ErrorCode, new ValidationSeverity()))
                                            .ToList();

            if (errors.Any())
            {
                return (dynamic)Result.Invalid(errors);
            }


            return await next();
        }
    }
}
