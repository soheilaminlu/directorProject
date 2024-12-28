using blogService.Dto;
using blogService.Models;

namespace blogService.Mappers
{
    public class BlogsMapper
    {
        public BlogsModel MapBlogsDtoToModel(BlogsDto blogsDto)
        {
            return new BlogsModel
            {
                Title = blogsDto.Title,
                Description = blogsDto.Description,
               CreatedDate = blogsDto.CreatedDate,
            };

        }
        public BlogsDto MapAddBlogsToDto(AddBlogsDto addBlogsDto)
        {
            return new BlogsDto
            {
                Title = addBlogsDto.Title,
                Description = addBlogsDto.Description,
            };
        }

        public BlogsDto MapUpdateBlogsToDto(UpdateBlogs updateBlogsDto)
        {
            return new BlogsDto
            {
                Title = updateBlogsDto.Title,
                Description = updateBlogsDto.Description,
            };
        }


    }
}
