using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using WebAPI_Assignment.Constants;

namespace WebAPI_Assignment.OpenApi;

public sealed class ApiKeyDocumentTransformer : IOpenApiDocumentTransformer
{
  public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
  {
    document.Components ??= new OpenApiComponents();

    document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

    document.Components.SecuritySchemes[ApiConstants.BearerScheme] = new OpenApiSecurityScheme
    {
      Type = SecuritySchemeType.Http,
      Scheme = "bearer",
      BearerFormat = "JWT",
      Description = "Bearer token."
    };

    document.Components.SecuritySchemes[ApiConstants.ApiKeyHeader] = new OpenApiSecurityScheme
    {
      Type = SecuritySchemeType.ApiKey,
      Name = ApiConstants.ApiKeyHeader,
      In = ParameterLocation.Header,
      Description = "API key."
    };

    document.Security ??= [];

    var bearerRequirement = new OpenApiSecurityRequirement
    {
      {
        new OpenApiSecuritySchemeReference(ApiConstants.BearerScheme, document),
        []
      }
    };

    var requirement = new OpenApiSecurityRequirement
    {
      {
        new OpenApiSecuritySchemeReference(ApiConstants.ApiKeyHeader, document),
        []
      }
    };

    if (!document.Security.Any(r => r.Keys.Any(k => k.Reference.Id == ApiConstants.BearerScheme)))
    {
      document.Security.Add(bearerRequirement);
    }

    if (!document.Security.Any(r => r.Keys.Any(k => k.Reference.Id == ApiConstants.ApiKeyHeader)))
    {
      document.Security.Add(requirement);
    }

    foreach (var operation in from path in document.Paths
                              where ApiConstants.ExcludedPaths.Any(p => path.Key.StartsWith(p, StringComparison.OrdinalIgnoreCase))
                              from operation in (IEnumerable<OpenApiOperation>?)path.Value.Operations?.Values ?? []
                              select operation)
    {
      operation.Security = [];
    }

    return Task.CompletedTask;
  }
}
