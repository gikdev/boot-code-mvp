using Backend.Api.Common;

namespace Backend.Api;

internal static class EndpointExtensions {
    internal static IEndpointRouteBuilder MapApiEndpoints<TMarker>(this IEndpointRouteBuilder app) {
        var typeMarker = typeof(TMarker);

        var endpointTypes = typeMarker.Assembly.DefinedTypes
            .Where(x =>
                !x.IsAbstract &&
                !x.IsInterface &&
                typeof(EndpointBase).IsAssignableFrom(x)
            );

        var methodName = nameof(EndpointBase.MapEndpoint);

        foreach (var endpointType in endpointTypes)
            endpointType
                .GetMethod(methodName)?
                .Invoke(null, [app]);

        return app;
    }
}
