using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Requests;

namespace WebAPI_Assignment.Repositories;

public interface INoteRepository
{
  Task<Note?> Add(Note item);
  Task<Note?> AddItem(string id, string itemId);
  Task<Note?> Delete(string id);
  Task<bool> Exists(CreateNoteRequest item, string userId);
  Task<List<Note>> GetAll(string userId);
  Task<Note?> GetById(string id, string userId);
  Task<Note?> Update(string id, Note item);
}
