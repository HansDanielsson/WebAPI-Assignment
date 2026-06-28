using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;
using WebAPI_Assignment.Services;

namespace WebAPI_Assignment.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CategoriesController(ICategoryService service) : ApiBaseController
{
  private readonly ICategoryService _service = service;

  [HttpPost]
  public async Task<ActionResult<CategoryDto>> Add([FromBody] CreateCategoryRequest item)
  {
    var result = await _service.Add(item, UserId);
    return result is null ? Conflict("Failed to create category") : CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
  }

  [HttpPost("{id}/notes/{itemId}")]
  public async Task<ActionResult<CategoryDto>> AddItem(string id, string itemId)
  {
    var result = await _service.AddItem(id, itemId, UserId);
    return result is null ? NotFound("The requested item was not found") : Ok(result);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(string id)
  {
    var item = await _service.Delete(id, UserId);
    return item is null ? NotFound("Failed to delete") : NoContent();
  }

  [HttpGet]
  public async Task<ActionResult<List<CategoryDto>>> GetAll() => await _service.GetAll(UserId);

  [HttpGet("{id}")]
  public async Task<ActionResult<CategoryDto>> GetById(string id)
  {
    var result = await _service.GetById(id, UserId);
    return result is null ? NotFound("The requested item was not found") : Ok(result);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<CategoryDto>> Update(string id, [FromBody] CreateCategoryRequest item)
  {
    var result = await _service.Update(id, item, UserId);
    return result is null ? NotFound("Failed to update") : Ok(result);
  }
}
