using System;
using ccore_api.Data;
using ccore_api.Entities;
using ccore_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ccore_api.Repositories;

public class BookRepository : IBook
{
    private readonly AppDBContext _context;

    public BookRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync(int pageNumber, int pageSize, string? filter)
    {
        var skipCount = (pageNumber - 1) * pageSize;

        return await FilterBook(filter)
                     .Include(book => book.Author)
                     .OrderBy(book => book.BookName)
                     .Skip(skipCount)
                     .Take(pageSize)
                     .AsNoTracking()
                     .ToListAsync();
    }

    public async Task<Book?> GetAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task CreateAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _context.Books.Where(book => book.Id == id)
                      .ExecuteDeleteAsync();
    }

    private IQueryable<Book> FilterBook(string? filter)
    {
        if(string.IsNullOrWhiteSpace(filter))
        {
            return _context.Books;
        }

        return _context.Books
                       .Where(book => book.BookName.Contains(filter)
                           || book.Genre.Contains(filter));
    }
}
