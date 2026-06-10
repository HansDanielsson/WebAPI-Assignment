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

  public async Task<Category?> Add(CreateCategoryRequest item, string userId)
  {
    if (await _repository.Exists(item, userId))
    {
      return null;
    }

    var itemDb = new Category(item.Name) { UserId = userId };
    return await _repository.Add(itemDb);
  }

  public async Task<Category?> AddItem(string id, string itemId) => await _repository.AddItem(id, itemId);

  public async Task<Category?> Delete(string id) => await _repository.Delete(id);

  public async Task<List<CategoryDto>> GetAll()
  {
    var items = await _repository.GetAll();

    return _mapper.Map<List<CategoryDto>>(items);
  }

  public async Task<CategoryDto> GetById(string id)
  {
    var item = await _repository.GetById(id);

    return _mapper.Map<CategoryDto>(item);
  }

  public async Task<Category?> Update(string id, CreateCategoryRequest item)
  {
    var itemDb = new Category(item.Name);
    return await _repository.Update(id, itemDb);
  }
}
