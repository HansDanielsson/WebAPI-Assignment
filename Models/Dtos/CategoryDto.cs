namespace WebAPI_Assignment.Models.Dtos;

public class CategoryDto
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public UserDto? User { get; set; }
  public int NoteCount { get; set; }
}
