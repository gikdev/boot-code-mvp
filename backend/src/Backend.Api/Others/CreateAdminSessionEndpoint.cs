using Backend.Api.Common;
using Backend.Contracts.Others;

using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Others;

internal class CreateAdminSessionEndpoint : EndpointBase {
    internal override string Name => "CreateAdminSession";

    internal override void MapEndpoint(IEndpointRouteBuilder app) {
        app
            .MapPost(ApiEndpoints.Others.CreateAdminSession, Handle)
            .WithName(Name)
            .WithSummary("Create a admin session (verify admin)")
            .WithTags(ApiTags.Others)
            .Accepts<CreateAdminSessionRequest>("application/json")
            .Produces<AdminSessionResponse>();
    }

    private async Task<IResult> Handle(
        [FromServices] ILogger<CreateAdminSessionEndpoint>   logger,
        [FromServices] ISender                               mediator,
        // [FromServices] IValidator<CreateAdminSessionRequest> validator,
        [FromBody]     CreateAdminSessionRequest             request
    ) {
        // var validationResult = await validator.ValidateAsync(request);
        // if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("Request received with {@Request}.", request);

        var isAdmin = await mediator.Send(request.MapToCommand());

        logger.LogInformation("Request succeeded with IsAdmin {IsAdmin}", isAdmin);
        return Results.Ok(Mappings.CreateAdminSessionResponse(isAdmin));
    }
}
