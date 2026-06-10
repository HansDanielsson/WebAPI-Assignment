using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI_Assignment.Models;

namespace WebAPI_Assignment.Contexts;

public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<User>(options)
{
}
