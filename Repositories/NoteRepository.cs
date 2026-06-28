using Microsoft.EntityFrameworkCore;
using WebAPI_Assignment.Contexts;
using WebAPI_Assignment.Models;
using WebAPI_Assignment.Models.Requests;

namespace WebAPI_Assignment.Repositories;

public class NoteRepository(ApplicationDbContext context) : INoteRepository
{
  private readonly ApplicationDbContext _context = context;

  public async Task<Note?> Add(Note item)
  {
    var result = await _context.Notes.AddAsync(item);
    await _context.SaveChangesAsync();

    return result.Entity;
  }

  public async Task<Note?> AddItem(string id, string itemId)
  {
    var result = await _context.Notes.FindAsync(id);
    if (result is null)
    {
      return null;
    }

    result.CategoryId = itemId;

    await _context.SaveChangesAsync();

    return result;
  }

  public async Task<Note?> Delete(string id)
  {
    var item = await _context.Notes.FindAsync(id);

    if (item is not null)
    {
      _context.Notes.Remove(item);
      await _context.SaveChangesAsync();
    }
    return item;
  }

  public async Task<bool> Exists(CreateNoteRequest item, string userId) => await _context.Notes.AnyAsync(s => s.Title == item.Title && s.UserId == userId);

  public async Task<List<Note>> GetAll(string userId) => await _context.Notes.Where(s => s.UserId == userId).Include(i => i.Category).Include(u => u.User).ToListAsync();

  public async Task<Note?> GetById(string id, string userId) => await _context.Notes.Where(s => s.Id == id && s.UserId == userId).Include(i => i.Category).Include(u => u.User).FirstOrDefaultAsync();

  public async Task<Note?> Update(string id, Note item)
  {
    var findId = await _context.Notes.FindAsync(id);

    if (findId is null)
    {
      return null;
    }

    var exists = await _context.Notes.AnyAsync(s => s.Title == item.Title && s.Id != findId.Id);
    if (exists)
    {
      return null;
    }

    findId.Title = item.Title;
    findId.Content = item.Content;
    findId.UpdatedAt = DateTime.Now;
    findId.CategoryId = item.CategoryId;

    _context.Notes.Update(findId);
    await _context.SaveChangesAsync();
    return findId;
  }
}
