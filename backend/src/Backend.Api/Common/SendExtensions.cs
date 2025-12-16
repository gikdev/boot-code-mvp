using ErrorOr;
using FastEndpoints;

namespace Backend.Api.Common;

public static class SendExtensions {
    public static Task SendErrorListAsync(this IResponseSender sender, List<Error> errors) {
        return sender.HttpContext.Response.SendResultAsync(ApiResults.Problem(errors));
    }
}
