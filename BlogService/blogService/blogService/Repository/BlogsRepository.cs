using blogService.Dto;
using blogService.Mappers;
using BlogsProject.Data;
using Microsoft.EntityFrameworkCore;

namespace blogService.Repository
{
    public class BlogsRepository : IBlogRepository
    {
        private readonly BlogsMapper _mapper;
        private readonly ApplicationDbContext _context;

        public BlogsRepository(BlogsMapper mapper , ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;

        }
        public async ValueTask<int> AddAsync(AddBlogsDto addBlogsDto)
        {
            var blogDto = _mapper.MapAddBlogsToDto(addBlogsDto);
            var blogModel = _mapper.MapBlogsDtoToModel(blogDto);
           await _context.Blogs.AddAsync(blogModel);
           await _context.SaveChangesAsync();
            return blogModel.Id;
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return false;
            }
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
            
        }
        public async ValueTask<List<BlogsDto>> GetBlogsAsync(CancellationToken cancellationToken)
        {
            var blogs = await _context.Blogs.Select(b => new BlogsDto
            {
                Title = b.Title,
                Description = b.Description,
                CreatedDate = b.CreatedDate,
                Id = b.Id,
            }).ToListAsync(cancellationToken);

            return blogs;

        }

        public async ValueTask<bool> UpdateAsync(int id, UpdateBlogs updateBlogsDto)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return false;
            }
           blog.Title = updateBlogsDto.Title;
           blog.Description = updateBlogsDto.Description;
           await _context.SaveChangesAsync();
            return true;
        }
    }
}
