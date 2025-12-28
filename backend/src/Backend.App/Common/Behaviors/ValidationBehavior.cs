using ErrorOr;

using FluentValidation;

using MediatR;

namespace Backend.App.Common.Behaviors;

internal class ValidationBehavior<TReq, TRes>(
    IValidator<TReq>? validator
) : IPipelineBehavior<TReq, TRes>
    where TReq : IRequest<TRes>
    where TRes : IErrorOr {
    public async Task<TRes> Handle(
        TReq                         request,
        RequestHandlerDelegate<TRes> next,
        CancellationToken            cancellationToken
    ) {
        if (validator is null) return await next(cancellationToken);

        var result = await validator.ValidateAsync(request, cancellationToken);
        if (result.IsValid) return await next(cancellationToken);

        var errors = result.Errors
            .Select(e => Error.Validation(
                e.PropertyName,
                e.ErrorMessage
            ))
            .ToList();

        return (dynamic)errors;
    }
}
