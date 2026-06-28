using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Assignment.Models;

namespace WebAPI_Assignment.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController(UserManager<User> userManager) : ApiBaseController
{
  private readonly UserManager<User> _userManager = userManager;

  [HttpPost("userapikey")]
  public async Task<ActionResult<string>> UserApiKey()
  {

    var user = await _userManager.FindByIdAsync(UserId);
    if (user is null)
    {
      return Unauthorized();
    }
    user.ApiKey = Guid.NewGuid().ToString();

    var result = await _userManager.UpdateAsync(user);
    if (!result.Succeeded)
    {
      return Problem(title: "Failed to update API key",
        statusCode: StatusCodes.Status500InternalServerError);
    }
    return Ok(user.ApiKey);
  }

  [HttpGet("userapikey")]
  public async Task<ActionResult<string>> GetApiKey()
  {
    var user = await _userManager.FindByIdAsync(UserId);
    if (user is null)
    {
      return Unauthorized();
    }
    return Ok(user.ApiKey);
  }

  [HttpPost("userclaims")]
  public async Task<IActionResult> UserClaims()
  {
    return Ok(User.Claims.Select(c => new
    {
      c.Type,
      c.Value
    }));
  }

  [HttpPost("userid")]
  public async Task<ActionResult<string?>> GetCurrentUserId()
  {
    return Ok(User.FindFirstValue(ClaimTypes.NameIdentifier));
  }
}
