using Backend.Api.Common;

namespace Backend.Api;

internal static class EndpointExtensions {
    internal static IEndpointRouteBuilder MapApiEndpoints<TMarker>(this IEndpointRouteBuilder app) {
        var typeMarker = typeof(TMarker);

        var endpointTypes = typeMarker.Assembly.DefinedTypes
            .Where(x =>
                !x.IsAbstract  &&
                !x.IsInterface &&
                typeof(EndpointBase).IsAssignableFrom(x)
            );

        foreach (var endpointType in endpointTypes) {
            var endpoint = Activator.CreateInstance(endpointType) as EndpointBase;
            endpoint?.MapEndpoint(app);
        }

        return app;
    }
}
