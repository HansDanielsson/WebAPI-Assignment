using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Requests;

namespace WebAPI_Assignment.Repositories;

public interface ICategoryRepository
{
  Task<Category?> Add(Category item);
  Task<Category?> AddItem(string id, string itemId);
  Task<Category?> Delete(string id);
  Task<bool> Exists(CreateCategoryRequest item, string userId);
  Task<List<Category>> GetAll();
  Task<Category?> GetById(string id);
  Task<Category?> Update(string id, Category item);
}
