using ErrorOr;

namespace Backend.Api.Common;

internal abstract class EndpointBase {
    internal abstract string Name { get; }
    internal abstract void MapEndpoint(IEndpointRouteBuilder app);

    protected static IResult Problem(List<Error> errors) {
        if (errors.Count == 0)
            return TypedResults.Problem();

        return errors.All(e => e.Type == ErrorType.Validation)
            ? ValidationProblem(errors)
            : Problem(errors[0]);
    }

    protected static IResult Problem(Error error) {
        var statusCode = error.Type switch {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return TypedResults.Problem(
            statusCode: statusCode,
            detail: error.Description
        );
    }

    protected static IResult ValidationProblem(List<Error> errors) {
        // Convert to the RFC 7807 validation dictionary format
        var errorDictionary = errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Description).ToArray()
            );

        return TypedResults.ValidationProblem(errorDictionary);
    }
}
