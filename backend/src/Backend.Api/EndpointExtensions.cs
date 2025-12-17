using Backend.Api.Lessons.Endpoints;

namespace Backend.Api;

public static class EndpointExtensions {
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app) {
        // Lessons
        app.MapCreateLesson();
        app.MapDeleteLessonById();
        app.MapGetLessonById();
        app.MapListLessons();
        app.MapUpdateLessonItselfById();

        return app;
    }
}
