using Backend.App.Common.Interfaces;
using Backend.Infra.Common.Persistence;
using Backend.Infra.Lessons.Persistence;
using Backend.Infra.Others.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infra;

public static class DependencyInjection {
    public static IServiceCollection AddInfraStuff(this IServiceCollection services) {
        services.AddDb();
        services.AddRepos();

        return services;
    }

    private static IServiceCollection AddDb(this IServiceCollection services) {
        services.AddDbContext<MainDbCtx>(options => options.UseSqlite("Data Source = Main.db"));

        return services;
    }

    private static IServiceCollection AddRepos(this IServiceCollection services) {
        services.AddScoped<ILessonsRepo, LessonsRepo>();
        services.AddScoped<IOthersRepo, OthersRepo>();

        return services;
    }
}
