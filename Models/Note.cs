namespace WebAPI_Assignment.Models;

/// <summary>
/// Användare kan skapa egna anteckningar
/// </summary>
public class Note(string title, string content, string categoryId, string userId)
{
  /// <summary>
  /// Anteckningsnyckel
  /// </summary>
  public string Id { get; set; } = Guid.NewGuid().ToString();

  /// <summary>
  /// Antecknings titel
  /// </summary>
  public string Title { get; set; } = title;

  /// <summary>
  /// Antecknings innehåll
  /// </summary>
  public string Content { get; set; } = content;

  /// <summary>
  /// Skapat datum
  /// </summary>
  public DateTime CreatedAt { get; init; } = DateTime.Now;

  /// <summary>
  /// Ändrat senast
  /// </summary>
  public DateTime UpdatedAt { get; set; } = DateTime.Now;

  /// <summary>
  /// Primärnyckel till Användar Id
  /// </summary>
  public string UserId { get; init; } = userId;

  /// <summary>
  /// Användare
  /// </summary>
  public User? User { get; set; }

  /// <summary>
  /// Primärnyckel till Kategori Id
  /// </summary>
  public string CategoryId { get; set; } = categoryId;

  /// <summary>
  /// Kategori
  /// </summary>
  public Category? Category { get; set; }
}
