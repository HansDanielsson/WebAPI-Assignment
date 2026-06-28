using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;

namespace WebAPI_Assignment.Services;

public interface ICategoryService
{
  Task<CategoryDto?> Add(CreateCategoryRequest item, string userId);
  Task<CategoryDto?> AddItem(string id, string itemId, string userId);
  Task<Category?> Delete(string id, string userId);
  Task<List<CategoryDto>> GetAll(string userId);
  Task<CategoryDto> GetById(string id, string userId);
  Task<CategoryDto?> Update(string id, CreateCategoryRequest item, string userId);
}
