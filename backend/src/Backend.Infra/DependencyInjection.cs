using Backend.App.Common.Interfaces;
using Backend.Infra.Common.Persistence;
using Backend.Infra.Lessons.Persistence;
using Backend.Infra.Others.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infra;

public static class DependencyInjection {
    public static IServiceCollection AddInfraStuff(this IServiceCollection services, string connStr) {
        services.AddDb(connStr);
        services.AddRepos();

        return services;
    }

    private static IServiceCollection AddDb(this IServiceCollection services, string connStr) {
        services.AddDbContext<MainDbCtx>(options => options.UseNpgsql(connStr));

        return services;
    }

    private static IServiceCollection AddRepos(this IServiceCollection services) {
        services.AddScoped<ILessonsRepo, LessonsRepo>();
        services.AddScoped<IOthersRepo, OthersRepo>();

        return services;
    }
}
