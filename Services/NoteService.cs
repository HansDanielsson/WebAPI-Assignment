using AutoMapper;
using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;
using WebAPI_Assignment.Repositories;

namespace WebAPI_Assignment.Services;

public class NoteService(INoteRepository noteRepo, ICategoryRepository categoryRepo, IMapper mapper) : INoteService
{
  private readonly INoteRepository _noteRepository = noteRepo;
  private readonly ICategoryRepository _categoryRepository = categoryRepo;
  private readonly IMapper _mapper = mapper;

  public async Task<NoteDto?> Add(CreateNoteRequest item, string userId)
  {
    if (await _noteRepository.Exists(item, userId))
    {
      return null;
    }

    var itemDb = new Note(item.Title, item.Content, item.CategoryId, userId);
    return _mapper.Map<NoteDto>(await _noteRepository.Add(itemDb));
  }

  public async Task<NoteDto?> AddItem(string id, string itemId, string userId)
  {
    var note = await _noteRepository.GetById(id, userId);
    if (note is null)
    {
      return null;
    }

    var category = await _categoryRepository.GetById(itemId, userId);
    if (category is null)
    {
      return null;
    }

    return _mapper.Map<NoteDto>(await _noteRepository.AddItem(id, itemId));
  }

  public async Task<Note?> Delete(string id, string userId)
  {
    var item = await _noteRepository.GetById(id, userId);
    if (item is null)
    {
      return null;
    }
    return await _noteRepository.Delete(id);
  }

  public async Task<List<NoteDto>> GetAll(string userId) => _mapper.Map<List<NoteDto>>(await _noteRepository.GetAll(userId));

  public async Task<NoteDto> GetById(string id, string userId) => _mapper.Map<NoteDto>(await _noteRepository.GetById(id, userId));

  public async Task<NoteDto?> Update(string id, CreateNoteRequest item, string userId)
  {
    var oldItem = await _noteRepository.GetById(id, userId);
    if (oldItem is null)
    {
      return null;
    }

    oldItem.Title = item.Title;
    oldItem.Content = item.Content;
    oldItem.CategoryId = item.CategoryId;

    return _mapper.Map<NoteDto>(await _noteRepository.Update(id, oldItem));
  }
}
