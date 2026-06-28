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
    var result = await _context.Categories.AddAsync(item);
    await _context.SaveChangesAsync();

    return result.Entity;
  }

  public async Task<Category?> AddItem(string id, string itemId, string userId)
  {
    var result = await _context.Categories.Include(c => c.Notes).FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
    if (result is null)
    {
      return null;
    }

    var item = await _context.Notes.FirstOrDefaultAsync(s => s.Id == itemId && s.UserId == userId);
    if (item is null)
    {
      return null;
    }

    if (!result.Notes.Any(n => n.Id == itemId))
    {
      result.Notes.Add(item);
    }

    await _context.SaveChangesAsync();

    await _context.Entry(result).Collection(c => c.Notes).LoadAsync();

    return result;
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

  public async Task<List<Category>> GetAll(string userId) => await _context.Categories.Where(s => s.UserId == userId).Include(u => u.User).Include(c => c.Notes).ToListAsync();

  public async Task<Category?> GetById(string id, string userId) => await _context.Categories.Where(s => s.Id == id && s.UserId == userId).Include(u => u.User).Include(c => c.Notes).FirstOrDefaultAsync();

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
