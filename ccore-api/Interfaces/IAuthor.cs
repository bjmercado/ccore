using ccore_api.Entities;

namespace ccore_api.Interfaces;

public interface IAuthor
{
    Task<IEnumerable<Author>> GetAllAsync(int pageNumber, int pageSize, string? filter);
    Task<Author?> GetAsync(int id);
    Task CreateAsync(Author author);
    Task UpdateAsync(Author autho);
    Task DeleteAsync(int id);
}
