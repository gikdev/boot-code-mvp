using System.Text.Json.Serialization;
using FluentValidation;
using Scalar.AspNetCore;
using Serilog;

namespace Backend.Api;

internal static class Setup {
    private const JsonNumberHandling jsonNumberHandling = JsonNumberHandling.Strict;

    internal static IServiceCollection AddApiStuff(this IServiceCollection services) {
        services.AddCors(o => {
            o.AddPolicy("DevCorsPolicy", policy => policy
                .WithOrigins(
                    "http://localhost:4263",
                    "http://127.0.0.1:4263"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );
        });

        services.ConfigureHttpJsonOptions(o => {
            o.SerializerOptions.NumberHandling = jsonNumberHandling;
        });

        services.AddOpenApi();
        services.AddProblemDetails();
        services.AddValidatorsFromAssemblyContaining<Program>();

        return services;
    }

    internal static WebApplication UseApiStuff(this WebApplication app) {
        app.UseExceptionHandler();
        // app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment()) app.UseCors("DevCorsPolicy");

        app.MapApiEndpoints<Program>();
        app.MapGet("/", () => Results.Redirect("/scalar"))
            .ExcludeFromDescription();

        app.MapOpenApi();
        app.MapScalarApiReference(o => o
            .WithTitle("BootCode MVP API")
            // .WithClassicLayout()
            .WithTheme(ScalarTheme.DeepSpace)
            .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Fetch)
        );

        return app;
    }

    internal static IHostBuilder ConfigLoggingStuff(this IHostBuilder host, IConfiguration config) {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();

        host.UseSerilog();

        return host;
    }
}
