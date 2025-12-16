using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Swagger;
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
        services
            .AddFastEndpoints()
            .SwaggerDocument();

        return services;
    }

    public static WebApplication UseApiStuff(this WebApplication app) {
        app.UseDefaultExceptionHandler();

        app.UseCors(o => o
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
        );

        app.UseFastEndpoints(c => {
            c.Serializer.Options.NumberHandling = jsonNumberHandling;
        }).UseSwaggerGen();

        // app.UseOpenApi(c => c.Path = "/openapi/{documentName}.json");
        // app.MapScalarApiReference();

        return app;
    }
}
