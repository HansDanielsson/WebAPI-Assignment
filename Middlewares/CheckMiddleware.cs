using Microsoft.AspNetCore.Identity;
using WebAPI_Assignment.Models;

namespace WebAPI_Assignment.Middlewares;

public sealed class CheckMiddleware(RequestDelegate next)
{
  private const string ApiKeyHeader = "X-Api-Key";

  private static readonly string[] ExcludedPaths = [
    "/login",
    "/register",
    "/user"
    ];

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

    if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var apiKey) || string.IsNullOrWhiteSpace(apiKey))
    {
      await Unauthorized(context, $"Header {ApiKeyHeader} is missing");
      return;
    }

    if (!string.Equals(user.ApiKey, apiKey, StringComparison.Ordinal))
    {
      await Unauthorized(context, $"Invalid API key, User:{user}, U.key: {user.ApiKey}, head key: {apiKey}");
      return;
    }
    await next(context);
  }

  private static bool ShouldSkip(PathString path) =>
    ExcludedPaths.Any(p => path.StartsWithSegments(p, StringComparison.OrdinalIgnoreCase));

  private sealed record ErrorResponse(string Message);

  private static Task Unauthorized(HttpContext context, string message)
  {
    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    context.Response.ContentType = "application/json";

    return context.Response.WriteAsJsonAsync(new ErrorResponse(message));
  }
}
