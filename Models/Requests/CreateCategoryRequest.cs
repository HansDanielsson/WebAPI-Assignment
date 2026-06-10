using System.ComponentModel.DataAnnotations;

namespace WebAPI_Assignment.Models.Requests;

public class CreateCategoryRequest
{
  /// <summary>
  /// Kategori Namn
  /// </summary>
  [StringLength(32, ErrorMessage = "Namn krävs", MinimumLength = 5)]
  public string Name { get; set; } = string.Empty;
}
