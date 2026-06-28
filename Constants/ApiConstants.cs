namespace WebAPI_Assignment.Constants;

public static class ApiConstants
{
  public static readonly string[] ExcludedPaths = ["/login", "/openapi", "/register", "/scalar", "/user"];

  public const string BearerScheme = "Bearer";
  public const string ApiKeyHeader = "X-Api-Key";
}
