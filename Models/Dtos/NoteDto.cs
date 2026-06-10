namespace WebAPI_Assignment.Models.Dtos;

public class NoteDto
{
  public string Id { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public UserDto? User { get; set; }
  public CategoryDto? Category { get; set; }
}
