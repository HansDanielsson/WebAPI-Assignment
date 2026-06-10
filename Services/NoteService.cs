using AutoMapper;
using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;
using WebAPI_Assignment.Repositories;

namespace WebAPI_Assignment.Services;

public class NoteService(INoteRepository repository, IMapper mapper) : INoteService
{
  private readonly INoteRepository _repository = repository;
  private readonly IMapper _mapper = mapper;

  public async Task<Note?> Add(CreateNoteRequest item, string userId)
  {
    if (await _repository.Exists(item, userId))
    {
      return null;
    }
    var itemDb = new Note(item.Title, item.Content, item.CategoryId);
    return await _repository.Add(itemDb);
  }

  public async Task<Note?> AddItem(string id, string itemId) => await _repository.AddItem(id, itemId);

  public async Task<Note?> Delete(string id) => await _repository.Delete(id);

  public async Task<List<NoteDto>> GetAll()
  {
    var items = await _repository.GetAll();

    return _mapper.Map<List<NoteDto>>(items);
  }

  public async Task<NoteDto> GetById(string id)
  {
    var item = await _repository.GetById(id);

    return _mapper.Map<NoteDto>(item);
  }

  public async Task<Note?> Update(string id, CreateNoteRequest item)
  {
    var itemDb = new Note(item.Title, item.Content, item.CategoryId);
    return await _repository.Update(id, itemDb);
  }
}
