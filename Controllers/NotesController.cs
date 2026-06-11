using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;
using WebAPI_Assignment.Services;

namespace WebAPI_Assignment.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class NotesController(INoteService service) : ControllerBase
{
  private readonly INoteService _service = service;

  [HttpPost]
  public async Task<ActionResult<NoteDto>> Add([FromBody] CreateNoteRequest item)
  {
    try
    {
      var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
      if (userName is null)
      {
        return Unauthorized("Fel att skapa ny post med användare");
      }

      var result = await _service.Add(item, userName);
      return result is null ? Conflict("Fel att skapa ny post") : CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    catch (Exception ex)
    {
      return BadRequest(ex);
    }
  }

  [HttpPost("{id}/category/{itemId}")]
  public async Task<ActionResult<NoteDto>> AddItem(string id, string itemId)
  {
    try
    {
      var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
      if (userName is null)
      {
        return Unauthorized("Fel att skapa ny post med användare");
      }

      var result = await _service.AddItem(id, itemId, userName);
      return result is null ? NotFound("Fel att ändra") : result;
    }
    catch (Exception ex)
    {
      return BadRequest(ex);
    }
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(string id)
  {
    try
    {
      var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
      if (userName is null)
      {
        return Unauthorized("Fel att skapa ny post med användare");
      }

      var item = await _service.Delete(id, userName);
      return item is null ? NotFound("Fel att ta bort") : NoContent();
    }
    catch (Exception ex)
    {
      return BadRequest(ex);
    }
  }

  [HttpGet]
  public async Task<ActionResult<List<NoteDto>>> GetAll() => await _service.GetAll();

  [HttpGet("{id}")]
  public async Task<ActionResult<NoteDto>> GetById(string id)
  {
    try
    {
      var result = await _service.GetById(id);
      return result is null ? NotFound("Hittade inte det du sökte efter") : result;
    }
    catch (Exception ex)
    {
      return BadRequest(ex);
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<NoteDto>> Update(string id, [FromBody] CreateNoteRequest item)
  {
    try
    {
      var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
      if (userName is null)
      {
        return Unauthorized("Fel att Uppdatera med användare");
      }

      var result = await _service.Update(id, item, userName);
      return result is null ? NotFound("Fel att Uppdatera") : result;
    }
    catch (Exception ex)
    {
      return BadRequest(ex);
    }
  }
}
