using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Assignment.Contexts;
using WebAPI_Assignment.Models.Dtos;

namespace WebAPI_Assignment.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class DebugController(IdentityContext context, IMapper mapper) : ControllerBase
{

  [HttpGet]
  public async Task<ActionResult<List<UserDto>>> GetUser()
  {
    var users = await context.Users.ToListAsync();
    return Ok(mapper.Map<List<UserDto>>(users));
  }
}
