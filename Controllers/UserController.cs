using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Assignment.Models;

namespace WebAPI_Assignment.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController(UserManager<User> userManager) : ControllerBase
{
  private readonly UserManager<User> _userManager = userManager;

  [HttpPost("userapikey")]
  public async Task<ActionResult<string>> UserApiKey()
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId is null)
    {
      return Unauthorized("User not found");
    }
    var user = await _userManager.FindByIdAsync(userId);
    if (user is null)
    {
      return Unauthorized("User not found");
    }
    user.ApiKey = Guid.NewGuid().ToString();
    await _userManager.UpdateAsync(user);
    return user.ApiKey;
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
  public async Task<IActionResult> UserId()
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    return Ok(userId);
  }
}
