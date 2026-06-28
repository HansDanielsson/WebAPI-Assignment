namespace WebAPI_Assignment.Models;

/// <summary>
/// Användare kan skapa egna kategorier till anteckningar
/// </summary>
public class Category(string name, string userId)
{
  /// <summary>
  /// Kategorinyckel
  /// </summary>
  public string Id { get; set; } = Guid.NewGuid().ToString();

  /// <summary>
  /// Kategori Namn
  /// </summary>
  public string Name { get; set; } = name;

  /// <summary>
  /// Koppling till Användar Id
  /// </summary>
  public string UserId { get; init; } = userId;

  /// <summary>
  /// Användare
  /// </summary>
  public User? User { get; set; }

  /// <summary>
  /// Koppling till anteckningar
  /// </summary>
  public ICollection<Note> Notes { get; set; } = [];
}
