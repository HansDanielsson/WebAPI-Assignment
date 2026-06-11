using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Assignment.Contexts;
using WebAPI_Assignment.Models;

namespace WebAPI_Assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class DebugController(IdentityContext context) : ControllerBase
{

  [HttpGet]
  public async Task<ActionResult<List<User>>> GetUser()
  {
    return await context.Users.ToListAsync();
  }
}
