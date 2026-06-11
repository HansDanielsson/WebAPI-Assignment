using AutoMapper;
using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;
using WebAPI_Assignment.Repositories;

namespace WebAPI_Assignment.Services;

public class CategoryService(ICategoryRepository repository, IMapper mapper) : ICategoryService
{
  private readonly ICategoryRepository _repository = repository;
  private readonly IMapper _mapper = mapper;

  public async Task<CategoryDto?> Add(CreateCategoryRequest item, string userName)
  {
    if (await _repository.Exists(item, userName))
    {
      return null;
    }

    var itemDb = new Category(item.Name) { UserId = userName };
    return _mapper.Map<CategoryDto>(await _repository.Add(itemDb));
  }

  public async Task<CategoryDto?> AddItem(string id, string itemId, string userName)
  {
    var item = await _repository.GetById(id);
    if (item is null || !string.Equals(item.UserId, userName, StringComparison.OrdinalIgnoreCase))
    {
      return null;
    }

    return _mapper.Map<CategoryDto>(await _repository.AddItem(id, itemId));
  }

  public async Task<Category?> Delete(string id, string userName)
  {
    var item = await _repository.GetById(id);
    if (item is null || !string.Equals(item.UserId, userName, StringComparison.OrdinalIgnoreCase))
    {
      return null;
    }
    return await _repository.Delete(id);
  }

  public async Task<List<CategoryDto>> GetAll() => _mapper.Map<List<CategoryDto>>(await _repository.GetAll());

  public async Task<CategoryDto> GetById(string id) => _mapper.Map<CategoryDto>(await _repository.GetById(id));

  public async Task<CategoryDto?> Update(string id, CreateCategoryRequest item, string userName)
  {
    var oldItem = await _repository.GetById(id);
    if (oldItem is null || !string.Equals(oldItem.UserId, userName, StringComparison.Ordinal))
    {
      return null;
    }

    oldItem.Name = item.Name;

    return _mapper.Map<CategoryDto>(await _repository.Update(id, oldItem));
  }
}
