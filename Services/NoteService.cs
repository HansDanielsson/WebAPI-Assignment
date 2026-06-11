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

  public async Task<Note?> Add(CreateNoteRequest item, string userName)
  {
    if (await _repository.Exists(item, userName))
    {
      return null;
    }

    var itemDb = new Note(item.Title, item.Content, item.CategoryId) { UserId = userName };
    return await _repository.Add(itemDb);
  }

  public async Task<Note?> AddItem(string id, string itemId, string userName)
  {
    var item = await _repository.GetById(id);
    if (item is null || !string.Equals(item.UserId, userName, StringComparison.OrdinalIgnoreCase))
    {
      return null;
    }

    return await _repository.AddItem(id, itemId);
  }

  public async Task<Note?> Delete(string id, string userName)
  {
    var item = await _repository.GetById(id);
    if (item is null || !string.Equals(item.UserId, userName, StringComparison.OrdinalIgnoreCase))
    {
      return null;
    }
    return await _repository.Delete(id);
  }

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

  public async Task<Note?> Update(string id, CreateNoteRequest item, string userName)
  {
    var oldItem = await _repository.GetById(id);
    if (oldItem is null || !string.Equals(oldItem.UserId, userName, StringComparison.Ordinal))
    {
      return null;
    }

    oldItem.Title = item.Title;
    oldItem.Content = item.Content;
    oldItem.CategoryId = item.CategoryId;

    return await _repository.Update(id, oldItem);
  }
}
