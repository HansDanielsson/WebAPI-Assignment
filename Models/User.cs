using Microsoft.AspNetCore.Identity;

namespace WebAPI_Assignment.Models;

public class User : IdentityUser
{
  public string ApiKey { get; set; } = Guid.NewGuid().ToString();

  public ICollection<Category> Categories { get; set; } = [];
  public ICollection<Note> Notes { get; set; } = [];
}
