using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;

namespace WebAPI_Assignment.Services;

public interface INoteService
{
  Task<Note?> Add(CreateNoteRequest item, string userName);
  Task<Note?> AddItem(string id, string itemId, string userName);
  Task<Note?> Delete(string id, string userName);
  Task<List<NoteDto>> GetAll();
  Task<NoteDto> GetById(string id);
  Task<Note?> Update(string id, CreateNoteRequest item, string userName);
}
