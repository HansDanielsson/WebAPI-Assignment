using Microsoft.AspNetCore.Identity;
using WebAPI_Assignment.Constants;
using WebAPI_Assignment.Models;

namespace WebAPI_Assignment.Middlewares;

public sealed class CheckMiddleware(RequestDelegate next, ILogger<CheckMiddleware> logger)
{
  public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
  {
    if (ShouldSkip(context.Request.Path))
    {
      await next(context);
      return;
    }

    if (context.User.Identity?.IsAuthenticated != true)
    {
      await Unauthorized(context, "User must be authenticated");
      return;
    }

    var user = await userManager.GetUserAsync(context.User);
    if (user is null)
    {
      await Unauthorized(context, "User not found");
      return;
    }

    if (string.IsNullOrWhiteSpace(user.ApiKey))
    {
      await Unauthorized(context, "User must have the ApiKey property set");
      return;
    }

    if (!context.Request.Headers.TryGetValue(ApiConstants.ApiKeyHeader, out var apiKey) || string.IsNullOrWhiteSpace(apiKey))
    {
      // Generic response; do no leak why
      logger.LogWarning("Header {HeaderName} is missing", ApiConstants.ApiKeyHeader);
      await Unauthorized(context, "Header is missing");
      return;
    }

    if (!string.Equals(user.ApiKey, apiKey, StringComparison.Ordinal))
    {
      // Generic response; do not leak why
      logger.LogWarning(
        "Invalid API key, User:{User}, UserKey: {UserKey}, HeaderKey: {HeaderKey}",
        user,
        user.ApiKey,
        apiKey);
      await Unauthorized(context, "Invalid API key");
      return;
    }
    await next(context);
  }

  private static bool ShouldSkip(PathString path) => ApiConstants.ExcludedPaths.Any(p =>
                                                      path.Value?.Equals(p, StringComparison.OrdinalIgnoreCase) == true ||
                                                      path.Value?.StartsWith(p + "/", StringComparison.OrdinalIgnoreCase) == true);

  private sealed record ErrorResponse(string Message);

  private static Task Unauthorized(HttpContext context, string message)
  {
    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    context.Response.ContentType = "application/json";

    return context.Response.WriteAsJsonAsync(new ErrorResponse(message));
  }
}
