using ccore_api.Entities;

namespace ccore_api.Interfaces;

public interface IBook
{
    Task<IEnumerable<Book>> GetAllAsync(int pageNumber, int pagSize, string? filter); 
    Task<Book?> GetAsync(int id);
    Task CreateAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int ind);
}
