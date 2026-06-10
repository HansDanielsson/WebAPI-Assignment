using Microsoft.EntityFrameworkCore;
using WebAPI_Assignment.Contexts;
using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Requests;

namespace WebAPI_Assignment.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
  private readonly ApplicationDbContext _context = context;

  public async Task<Category?> Add(Category item)
  {
    try
    {
      var result = await _context.Categories.AddAsync(item);
      var change = await _context.SaveChangesAsync();

      return (change > 0) ? result.Entity : null;
    }
    catch
    {
      return null;
    }
  }

  public async Task<Category?> AddItem(string id, string itemId)
  {
    try
    {
      var result = await _context.Categories.Include(i => i.Notes).FirstOrDefaultAsync(s => s.Id == id);
      if (result is null)
      {
        return null;
      }

      var item = await _context.Notes.FindAsync(itemId);
      if (item is null)
      {
        return null;
      }

      if (result.Notes.Any(s => s.Id == itemId))
      {
        return null;
      }

      result.Notes.Add(item);

      var changes = await _context.SaveChangesAsync();

      return (changes > 0) ? result : null;
    }
    catch
    {
      return null;
    }
  }

  public async Task<Category?> Delete(string id)
  {
    var item = await _context.Categories.FindAsync(id);

    if (item is not null)
    {
      _context.Categories.Remove(item);
      await _context.SaveChangesAsync();
    }
    return item;
  }

  public async Task<bool> Exists(CreateCategoryRequest item, string userId) => await _context.Categories.AnyAsync(s => s.Name == item.Name && s.UserId == userId);

  public async Task<List<Category>> GetAll() => await _context.Categories.Include(i => i.Notes).Include(u => u.User).ToListAsync();

  public async Task<Category?> GetById(string id) => await _context.Categories.Include(i => i.Notes).Include(u => u.User).FirstOrDefaultAsync(s => s.Id == id);

  public async Task<Category?> Update(string id, Category item)
  {
    var findId = await _context.Categories.FindAsync(id);

    if (findId is null)
    {
      return null;
    }

    var exists = await _context.Categories.AnyAsync(s => s.Name == item.Name && s.Id != findId.Id);
    if (exists)
    {
      return null;
    }

    findId.Name = item.Name;

    _context.Categories.Update(findId);
    await _context.SaveChangesAsync();
    return findId;
  }
}
