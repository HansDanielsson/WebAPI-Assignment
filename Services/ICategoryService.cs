using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;

namespace WebAPI_Assignment.Services;

public interface ICategoryService
{
  Task<Category?> Add(CreateCategoryRequest item, string userId);
  Task<Category?> AddItem(string id, string itemId);
  Task<Category?> Delete(string id);
  Task<List<CategoryDto>> GetAll();
  Task<CategoryDto> GetById(string id);
  Task<Category?> Update(string id, CreateCategoryRequest item);
}
