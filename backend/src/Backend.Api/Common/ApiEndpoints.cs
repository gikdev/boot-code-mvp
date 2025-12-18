namespace Backend.Api.Common;

internal static class ApiEndpoints {
    private const string ApiBase = "api";

    internal static class Lessons {
        private const string Base = $"{ApiBase}/lessons";

        internal const string Create = $"{Base}";
        internal const string List = $"{Base}";

        internal const string ChangePositions = $"{Base}/positions";

        internal const string GetById = $"{Base}/{{id:guid}}";
        internal const string UpdateItselfById = $"{Base}/{{id:guid}}";
        internal const string DeleteById = $"{Base}/{{id:guid}}";

        internal const string UpdateContentById = $"{Base}/{{id:guid}}/content";
    }
}
