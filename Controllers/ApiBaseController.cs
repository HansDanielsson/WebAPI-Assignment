using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Assignment.Controllers;

public abstract class ApiBaseController : ControllerBase
{
  protected string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException();
}
