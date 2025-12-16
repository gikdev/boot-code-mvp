namespace Backend.Api.Common;

public static class ApiEndpoints {
    private const string ApiBase = "api";

    public static class Lessons {
        private const string Base = $"{ApiBase}/lessons";

        public const string Create = $"{Base}";
        public const string List = $"{Base}";

        public const string GetById = $"{Base}/{{id:guid}}";
        public const string DeleteById = $"{Base}/{{id:guid}}";
    }
}
