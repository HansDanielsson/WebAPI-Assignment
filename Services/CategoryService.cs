using AutoMapper;
using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;
using WebAPI_Assignment.Repositories;

namespace WebAPI_Assignment.Services;

public class CategoryService(ICategoryRepository categoryRepo, IMapper mapper) : ICategoryService
{
  private readonly ICategoryRepository _categoryRepository = categoryRepo;
  private readonly IMapper _mapper = mapper;

  public async Task<CategoryDto?> Add(CreateCategoryRequest item, string userId)
  {
    if (await _categoryRepository.Exists(item, userId))
    {
      return null;
    }

    var itemDb = new Category(item.Name, userId);
    return _mapper.Map<CategoryDto>(await _categoryRepository.Add(itemDb));
  }

  public async Task<CategoryDto?> AddItem(string id, string itemId, string userId) => _mapper.Map<CategoryDto>(await _categoryRepository.AddItem(id, itemId, userId));

  public async Task<Category?> Delete(string id, string userId)
  {
    var item = await _categoryRepository.GetById(id, userId);
    if (item is null)
    {
      return null;
    }
    return await _categoryRepository.Delete(id);
  }

  public async Task<List<CategoryDto>> GetAll(string userId) => _mapper.Map<List<CategoryDto>>(await _categoryRepository.GetAll(userId));

  public async Task<CategoryDto> GetById(string id, string userId) => _mapper.Map<CategoryDto>(await _categoryRepository.GetById(id, userId));

  public async Task<CategoryDto?> Update(string id, CreateCategoryRequest item, string userId)
  {
    var oldItem = await _categoryRepository.GetById(id, userId);
    if (oldItem is null)
    {
      return null;
    }

    oldItem.Name = item.Name;

    return _mapper.Map<CategoryDto>(await _categoryRepository.Update(id, oldItem));
  }
}
