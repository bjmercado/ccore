using ccore_api.Data;
using ccore_api.Entities;
using ccore_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ccore_api.Repositories;

public class AuthorRepository : IAuthor
{
    private readonly AppDBContext _context;
    public AuthorRepository(
        AppDBContext context
    )
    {
        _context = context;
    }
    public async Task<IEnumerable<Author>> GetAllAsync(int pageNumber, int pageSize, string? filter)
    {
        var skipCount = (pageNumber - 1) * pageSize;
        
        return await FilterAuthor(filter)
                     .Include(author => author.Books)
                     .OrderBy(author => author.LastName)
                     .Skip(skipCount)
                     .Take(pageSize)
                     .AsNoTracking()
                     .ToListAsync();
    }

    public async Task<Author?> GetAsync(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public async Task CreateAsync(Author author)
    {
        _context.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author author)
    {
        _context.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
       await _context.Authors.Where(author => author.Id == id)
                        .ExecuteDeleteAsync();
    }

    private IQueryable<Author> FilterAuthor(string? filter)
    {
        if(string.IsNullOrWhiteSpace(filter))
        {
            return _context.Authors;
        }
        
        return _context.Authors
                        .Where(author => author.FirstName.Contains(filter) 
                            || author.MiddleName.Contains(filter)
                            || author.LastName.Contains(filter));
    }
}
