using blogService.Dto;

namespace blogService.Repository
{
    public interface IBlogRepository
    {
        ValueTask<List<BlogsDto>> GetBlogsAsync(CancellationToken cancellationToken);
        ValueTask<int> AddAsync(AddBlogsDto addBlogsDto);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<bool> UpdateAsync(int id, UpdateBlogs updateBlogsDto);
    }
}
