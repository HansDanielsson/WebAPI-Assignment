using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Dtos;
using WebAPI_Assignment.Models.Requests;

namespace WebAPI_Assignment.Services;

public interface ICategoryService
{
  Task<Category?> Add(CreateCategoryRequest item, string userName);
  Task<Category?> AddItem(string id, string itemId, string userName);
  Task<Category?> Delete(string id, string userName);
  Task<List<CategoryDto>> GetAll();
  Task<CategoryDto> GetById(string id);
  Task<CategoryDto?> Update(string id, CreateCategoryRequest item, string userName);
}
