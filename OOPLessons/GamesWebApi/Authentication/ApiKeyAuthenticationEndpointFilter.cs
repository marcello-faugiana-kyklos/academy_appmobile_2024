using OOP.Common;

namespace GamesWebApi.Authentication;

internal class ApiKeyAuthenticationEndpointFilter : IEndpointFilter
{
    private const string ApiKeyConfName = "ApiKey";
    private const string ApiKeyHeaderName = "X-Api-Key";

    private readonly string _apiKey;

    public ApiKeyAuthenticationEndpointFilter(IConfiguration configuration)
    {
        string? apiKey = configuration.GetValue<string>(ApiKeyConfName);
        if (apiKey is null)
        {
            throw new Exception($"Invalid service configuration. No {ApiKeyConfName} provided");
        }
        _apiKey = apiKey;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        string? userApiKey = context.HttpContext?.Request?.Headers[ApiKeyHeaderName];

        if (!userApiKey.HasText())
        {
            return Results.BadRequest();
        }

        if (!IsApiKeyValid(userApiKey!))
        {
            return Results.Unauthorized();
        }

        return await next(context).ConfigureAwait(false);
    }

    private bool IsApiKeyValid(string userApiKey) =>
        userApiKey == _apiKey;
}

