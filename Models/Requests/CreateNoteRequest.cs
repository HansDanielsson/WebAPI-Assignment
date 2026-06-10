using System.ComponentModel.DataAnnotations;

namespace WebAPI_Assignment.Models.Requests;

public class CreateNoteRequest : IValidatableObject
{
  /// <summary>
  /// Antecknings titel
  /// </summary>
  [StringLength(32, ErrorMessage = "Titel krävs", MinimumLength = 5)]
  public string Title { get; set; } = string.Empty;

  /// <summary>
  /// Antecknings innehåll
  /// </summary>
  [StringLength(128, ErrorMessage = "Innehåll krävs", MinimumLength = 5)]
  public string Content { get; set; } = string.Empty;

  /// <summary>
  /// Primärnyckel till Kategori Id
  /// </summary>
  [Required(ErrorMessage = "CategoryId krävs")]
  public string CategoryId { get; set; } = string.Empty;

  public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    if (Title == Content)
    {
      yield return new ValidationResult("Titel och innehåll får inte vara samma.");
    }
  }
}
