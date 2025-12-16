using System.Text.Json.Serialization;
using FastEndpoints;
using Scalar.AspNetCore;

namespace Backend.Api;

public static class DependencyInjection {
    private const JsonNumberHandling jsonNumberHandling = JsonNumberHandling.Strict;

    public static IServiceCollection AddApiStuff(this IServiceCollection services) {
        services.ConfigureHttpJsonOptions(o => {
            o.SerializerOptions.NumberHandling = jsonNumberHandling;
        });

        services.AddProblemDetails();
        services.AddCors();
        services.AddOpenApi();
        services.AddFastEndpoints();

        return services;
    }

    public static WebApplication UseApiStuff(this WebApplication app) {
        app.UseDefaultExceptionHandler();

        app.UseCors(o => o
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
        );

        app.MapOpenApi();
        app.MapScalarApiReference();

        app.MapFastEndpoints(c => {
            c.Serializer.Options.NumberHandling = jsonNumberHandling;
        });

        return app;
    }
}
