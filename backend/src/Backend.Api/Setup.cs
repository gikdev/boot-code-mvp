using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using Serilog;

namespace Backend.Api;

public static class Setup {
    private const JsonNumberHandling jsonNumberHandling = JsonNumberHandling.Strict;

    public static IServiceCollection AddApiStuff(this IServiceCollection services) {
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

        return services;
    }

    public static WebApplication UseApiStuff(this WebApplication app) {
        app.UseExceptionHandler();
        // app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment()) app.UseCors("DevCorsPolicy");

        app.MapApiEndpoints();

        app.MapOpenApi();
        app.MapScalarApiReference(o => {
            o.Title = "BootCode MVP API";
            // o.Layout = ScalarLayout.Classic;
            o.Theme = ScalarTheme.DeepSpace;
        });

        return app;
    }

    public static IHostBuilder ConfigLoggingStuff(this IHostBuilder host, IConfiguration config) {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();

        host.UseSerilog();

        return host;
    }
}
