using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;

namespace WebAPI_Assignment.Services;

public interface INoteService
{
  Task<NoteDto?> Add(CreateNoteRequest item, string userId);
  Task<NoteDto?> AddItem(string id, string itemId, string userId);
  Task<Note?> Delete(string id, string userId);
  Task<List<NoteDto>> GetAll(string userId);
  Task<NoteDto> GetById(string id, string userId);
  Task<NoteDto?> Update(string id, CreateNoteRequest item, string userId);
}
